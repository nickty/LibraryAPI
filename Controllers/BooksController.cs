using LibraryAPI.Data;
using LibraryAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryAPI.Controllers
{
    [Route("api/[controller]")]  // This route is expecting /api/books
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly LibraryDbContext _context;

        // Injecting LibraryDbContext through constructor
        public BooksController(LibraryDbContext context)
        {
            _context = context;
        }

        // GET api/books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            var books = await _context.Books.Include(b => b.Author).ToListAsync(); // Includes author details
            return Ok(books);
        }

        // GET api/books/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _context.Books.Include(b => b.Author).FirstOrDefaultAsync(b => b.BookID == id);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        // POST api/books
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Book>> AddBook([FromBody] Book book)
        {
            if (book == null)
            {
                return BadRequest("Book data is invalid.");
            }

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBook), new { id = book.BookID }, book);
        }

        // DELETE api/books/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

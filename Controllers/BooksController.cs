using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace LibraryAPI.Controllers
{
    [Route("api/[controller]")]  // This route is expecting /api/books
    [ApiController]
    public class BooksController : ControllerBase
    {
        private static List<string> Books = new List<string> { "Book 1", "Book 2", "Book 3" };

        // GET api/books
        [HttpGet]
        public ActionResult<IEnumerable<string>> GetBooks()
        {
            return Ok(Books);
        }

        // GET api/books/{id}
        [HttpGet("{id}")]
        public ActionResult<string> GetBook(int id)
        {
            if (id < 0 || id >= Books.Count)
            {
                return NotFound();
            }
            return Ok(Books[id]);
        }

        // POST api/books
        [HttpPost]
        public ActionResult AddBook([FromBody] string bookName)
        {
            Books.Add(bookName);
            return CreatedAtAction(nameof(GetBooks), new { id = Books.Count - 1 }, bookName);
        }

        // DELETE api/books/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteBook(int id)
        {
            if (id < 0 || id >= Books.Count)
            {
                return NotFound();
            }
            Books.RemoveAt(id);
            return NoContent();
        }
    }
}

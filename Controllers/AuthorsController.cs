using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryAPI.Data;
using LibraryAPI.Models;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Authorization;



[ApiController]
[Route("api/[controller]")]
public class AuthorsController : ControllerBase
{
    private readonly LibraryDbContext _context;

    public AuthorsController(LibraryDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAuthors()
    {
        var authors = await _context.Authors.ToListAsync();
        return Ok(authors);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddAuthor(Author author)
    {
        _context.Authors.Add(author);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetAuthors), new { id = author.AuthorID }, author);
    }
}

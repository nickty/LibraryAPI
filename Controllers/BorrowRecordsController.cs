using LibraryAPI.Data;
using LibraryAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowRecordsController : ControllerBase
    {
        private readonly LibraryDbContext _context;

        // Injecting LibraryDbContext
        public BorrowRecordsController(LibraryDbContext context)
        {
            _context = context;
        }

        // GET api/borrowrecords
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BorrowRecord>>> GetBorrowRecords()
        {
            var borrowRecords = await _context.BorrowRecords
                .Include(br => br.Book)
                .Include(br => br.Member)
                .ToListAsync();

            return Ok(borrowRecords);
        }

        // GET api/borrowrecords/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<BorrowRecord>> GetBorrowRecord(int id)
        {
            var borrowRecord = await _context.BorrowRecords
                .Include(br => br.Book)
                .Include(br => br.Member)
                .FirstOrDefaultAsync(br => br.RecordID == id);

            if (borrowRecord == null)
            {
                return NotFound();
            }

            return Ok(borrowRecord);
        }

        // POST api/borrowrecords
        [HttpPost]
        public async Task<ActionResult<BorrowRecord>> AddBorrowRecord([FromBody] BorrowRecord borrowRecord)
        {
            if (borrowRecord == null)
            {
                return BadRequest("Invalid borrow record data.");
            }

            _context.BorrowRecords.Add(borrowRecord);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBorrowRecord), new { id = borrowRecord.RecordID }, borrowRecord);
        }

        // PUT api/borrowrecords/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBorrowRecord(int id, [FromBody] BorrowRecord borrowRecord)
        {
            if (id != borrowRecord.RecordID)
            {
                return BadRequest("Record ID mismatch.");
            }

            _context.Entry(borrowRecord).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE api/borrowrecords/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBorrowRecord(int id)
        {
            var borrowRecord = await _context.BorrowRecords.FindAsync(id);
            if (borrowRecord == null)
            {
                return NotFound();
            }

            _context.BorrowRecords.Remove(borrowRecord);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

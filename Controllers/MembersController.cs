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
    public class MembersController : ControllerBase
    {
        private readonly LibraryDbContext _context;

        // Injecting LibraryDbContext
        public MembersController(LibraryDbContext context)
        {
            _context = context;
        }

        // GET api/members
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Member>>> GetMembers()
        {
            var members = await _context.Members.ToListAsync();
            return Ok(members);
        }

        // GET api/members/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Member>> GetMember(int id)
        {
            var member = await _context.Members.FindAsync(id);

            if (member == null)
            {
                return NotFound();
            }

            return Ok(member);
        }

        // POST api/members
        [HttpPost]
        public async Task<ActionResult<Member>> AddMember([FromBody] Member member)
        {
            if (member == null)
            {
                return BadRequest("Invalid member data.");
            }

            _context.Members.Add(member);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMember), new { id = member.MemberID }, member);
        }

        // PUT api/members/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMember(int id, [FromBody] Member member)
        {
            if (id != member.MemberID)
            {
                return BadRequest("Member ID mismatch.");
            }

            _context.Entry(member).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE api/members/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMember(int id)
        {
            var member = await _context.Members.FindAsync(id);
            if (member == null)
            {
                return NotFound();
            }

            _context.Members.Remove(member);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

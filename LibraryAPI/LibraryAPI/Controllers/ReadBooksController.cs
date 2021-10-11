using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryAPI.Models;

namespace LibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReadBooksController : ControllerBase
    {
        private readonly LibraryContext _context;

        public ReadBooksController(LibraryContext context)
        {
            _context = context;
        }

        // GET: api/ReadBooks
        [HttpGet("name")]
        public async Task<ActionResult<IEnumerable<ReadBook>>> GetReadBooks(string name)
        {
            return  await _context.ReadBooks.Where(b=>b.Username==name).ToListAsync();

            /*foreach(var readBook in readBooks)
            {
                if (readBook.Username != name)
                    readBooks.Remove(readBook);
            }

            return readBooks;*/
        }

        // GET: api/ReadBooks/5
        [HttpGet("{name}/{id}")]
        public async Task<ActionResult<ReadBook>> GetReadBook(int id, string name)
        {
            var readBook = await _context.ReadBooks.FindAsync(name, id);

            if (readBook == null)
            {
                return NotFound();
            }

            return readBook;
        }

        // PUT: api/ReadBooks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReadBook(string id, ReadBook readBook)
        {
            if (id != readBook.Username)
            {
                return BadRequest();
            }

            _context.Entry(readBook).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReadBookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ReadBooks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ReadBook>> PostReadBook(ReadBook readBook)
        {
            _context.ReadBooks.Add(readBook);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ReadBookExists(readBook.Username))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetReadBook", new { id = readBook.Username }, readBook);
        }

        // DELETE: api/ReadBooks/5
        [HttpDelete("{name}/{id}")]
        public async Task<IActionResult> DeleteReadBook(string name, int id)
        {
            var readBook = await _context.ReadBooks.FindAsync(name, id);
            if (readBook == null)
            {
                return NotFound();
            }

            _context.ReadBooks.Remove(readBook);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReadBookExists(string id)
        {
            return _context.ReadBooks.Any(e => e.Username == id);
        }
    }
}

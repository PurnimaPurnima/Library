using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.IO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly LibraryContext _context;

        public BooksController(LibraryContext context)
        {
            _context = context;
        }

        // GET: api/<BooksController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> Get()
        {
            return await _context.Books.ToListAsync();
        }

        // GET api/<BooksController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        // POST api/<BooksController>
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBook", new { id = book.Id }, book);
        }

        // PUT api/<BooksController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, Book book)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }

            _context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
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


        // DELETE api/<BooksController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            int i;

            if (book == null)
            {
                return NotFound();
            }

            //int length = book.ImageUrl.Length;

            //for (i = length - 1; book.ImageUrl[i] != '/'; i--) ;

            string thumbnailName = book.ImageUrl.Substring(17);
            string pdfName = book.PdfUrl.Substring(15);

            var imagePath = Path.Combine(@"E:\Academics\LTI\CLD\Angular\LibraryProject\LibraryFrontend\src\assets\Images", thumbnailName);
            var pdfPath = Path.Combine(@"E:\Academics\LTI\CLD\Angular\LibraryProject\LibraryFrontend\src\assets\PDFs", pdfName);

            FileInfo imageFile = new FileInfo(imagePath);
            FileInfo pdfFile = new FileInfo(pdfPath);

            imageFile.Delete();
            pdfFile.Delete();

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PdfUploadController : ControllerBase
    {
        // POST api/<PdfUploadController>
        [HttpPost]
        public async Task UploadPdf(IFormFile file)
        {
            var filePath = Path.Combine(@"E:\Academics\LTI\CLD\Angular\LibraryProject\LibraryFrontend\src\assets\PDFs", file.FileName);

            await using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
        }

    }
}

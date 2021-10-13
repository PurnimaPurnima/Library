using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace LibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageUploadController : ControllerBase
    {
        [HttpPost]
        public async Task UploadImage(IFormFile file)
        {
            var filePath = Path.Combine(@"E:\Academics\LTI\CLD\Angular\LibraryProject\LibraryFrontend\src\assets\Images", file.FileName);
            
            await using (var stream=new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
        }

        /*[HttpDelete("{name}")]
        public async Task DeleteImage(string name)
        {
            var path = Path.Combine(@"E:\Academics\LTI\CLD\Angular\LibraryProject\LibraryFrontend\src\assets\Images", name);

            FileInfo file = new FileInfo(path);

            file.Delete();

            return 
        }*/
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using Microsoft.Net.Http.Headers;

namespace Host.Controllers
{
    [Route("api/[controller]")]
    public class FileController : Controller
    {
        [HttpPost]
        public IActionResult Post(ICollection<IFormFile> files)
        {
            var i = 0;
            foreach (var file in Request.Form.Files)
            {
                var header = ContentDispositionHeaderValue.Parse(file.ContentDisposition);
                using (var output = System.IO.File.Create(header.FileName))
                {
                    using (var input = file.OpenReadStream())
                    {
                        input.CopyTo(output);
                    }                        
                }
            }
            return new HttpOkResult();
        }
    }
}
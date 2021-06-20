using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;

namespace Education.Web.Controllers {
    [ApiController]
    [Route("/api/[controller]")]
    public abstract class BaseController : ControllerBase { }

    public class FilesController : BaseController {
        private IWebHostEnvironment _webHostEnvironment;
        private const string FileName = "Assets/face.jpg";
        private const string FileType = "image/jpg";

        public FilesController(IWebHostEnvironment webHostEnvironment) {
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("get-number")]
        public int GetNumber() {
            return 1;
        }

        [HttpGet]
        public IActionResult Get() {
            var result = GetFileByBase64();

            return result;
        }

        [HttpGet("number")]
        [ResponseCache(Duration = 10)]
        public int Number() => (new Random().Next(0, 10000));

        private IActionResult GetFileByPhysicalFileResult() {
            var filePath = Path.Combine(_webHostEnvironment.ContentRootPath, FileName);
            return PhysicalFile(filePath, FileType);
        }
        
        private IActionResult GetFileByBytes() {
            var path = Path.Combine(_webHostEnvironment.ContentRootPath, FileName);
            var bytes = System.IO.File.ReadAllBytes(path);
            return File(bytes, FileType);
        }

        private IActionResult GetFileByBase64() {
            var path = Path.Combine(_webHostEnvironment.ContentRootPath, FileName);
            var bytes = System.IO.File.ReadAllBytes(path);
            var base64 = Convert.ToBase64String(bytes);

            return Ok(base64);
        }

    }
}

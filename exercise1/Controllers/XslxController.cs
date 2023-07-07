using Microsoft.AspNetCore.Mvc;
using System.Text;
using exercise1.Services;

namespace exercise1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    
    public class XslxController : Controller
    {
        private readonly FileService _fileService;


        public XslxController(FileService fileService)
        {
            _fileService = fileService;
        }

        [HttpGet("Get")]
        public async Task<IActionResult> GetFile(int id)
        {
            var result = _fileService.Get(id);
            if (result == null)
                return BadRequest("There is not such a file with this id");
            return File(Encoding.UTF8.GetBytes(result.Json.ToString()), "text/csv", result.Name);
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile csv)
        {
                await _fileService.Upload(csv);

            return Ok("ok");
        }
    }
}

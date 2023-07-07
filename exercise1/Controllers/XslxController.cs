using exercise1.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Text.Json;
using System.Xml;
using static System.Net.Mime.MediaTypeNames;
using System.Text;
using ChoETL;
using System.IO;
using Microsoft.AspNetCore.Mvc.Razor.Infrastructure;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using exercise1.Models;
using exercise1.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using exercise1.Services;

namespace exercise1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class XslxController : Controller
    {
        private readonly XslxService _xslx;
        private readonly DataContext _context;


        public XslxController(XslxService xslx, DataContext context)
        {
            _xslx = xslx;
            _context = context;

        }

        [HttpGet]
        public async Task<IActionResult> GetXslx(int id)
        {
            var result = _xslx.Get(id);
            if (result == null)
                return BadRequest("There is not such a file with this id");
            return File(Encoding.UTF8.GetBytes(result.csvData.ToString()), "text/csv", result.Name);
        }

        [HttpPost]
        public async Task<IActionResult> UploadXslx(IFormFile csv)
        {
                await _xslx.Upload(csv);

            return Ok("ok");
        }
    }
}

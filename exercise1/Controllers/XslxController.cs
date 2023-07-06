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

namespace exercise1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class XslxController : Controller
    {
        private readonly IXslx _xslx;
        private readonly DataContext _context;


        public XslxController(IXslx xslx, DataContext context)
        {
            _xslx = xslx;
            _context = context;

        }

        [HttpGet]
        public async Task<IActionResult> GetXslx(int id)
        {
            var result = _xslx.GetXslx(id);
            return File(Encoding.UTF8.GetBytes(result.csvData.ToString()), "text/csv", "skrt");
        }

        [HttpPost]
        public async Task<IActionResult> UploadXslx(IFormFile csv)
        {
                var csvInBase = await _context.Csv
                .Where(w => w.Name == csv.FileName)
                .FirstOrDefaultAsync();

            StringBuilder sb = new StringBuilder();
            using (var p = new ChoCSVReader(csv.OpenReadStream())
                .WithFirstLineHeader()
                )
            {
                using (var w = new ChoJSONWriter(sb))
                    w.Write(p);
            }

            if (csvInBase == null)
            {
                var tmpCsv = new Xslx
                {
                    Name = csv.FileName,
                    csvData = sb.ToString(),
                    inserttimetamp = DateTime.UtcNow
                };
                _context.Csv.Add(tmpCsv);
            } else
            {
                csvInBase.csvData = sb.ToString();
                csvInBase.inserttimetamp = DateTime.UtcNow;
            }
            
            await _context.SaveChangesAsync();

            return Ok("ok");
        }
    }
}

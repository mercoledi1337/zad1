using ChoETL;
using exercise1.Data;
using exercise1.Interfaces;
using exercise1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace exercise1.Repository
{
    public class XslxRepository : IXslx
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public XslxRepository(DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;

        }
        //public Xslx GetXslx(int id)
        //{
        //    return _context.Csv.Where(u => u.Id == id).FirstOrDefault();
        //}

        public Task<IActionResult> UploadXslx(IFormFileCollection csv)
        {
            throw new NotImplementedException();
        }

        public XslxresultClass GetXslx(int Id)
        {
            var csv = _context.Csv.Where(c => c.Id == Id).FirstOrDefault();

            StringBuilder tmp = new StringBuilder();
            using (var r = ChoJSONReader.LoadText(csv.csvData))
            {
                using (var w = new ChoCSVWriter(tmp).WithFirstLineHeader())
                {
                    w.Write(r);
                }
            }
            


            XslxresultClass restult = new()
            {
                Id = csv.Id,
                Name = csv.Name,
                csvData = tmp
        };
            return restult;
        }
    }
}

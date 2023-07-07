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

        public async Task<Xslx> Get(IFormFile csv)
        {
            return await _context.Csv.FirstOrDefaultAsync(w => w.Name == csv.FileName);
        }

        public async Task Save(Xslx csv)
        {
            
            await _context.Csv.AddAsync(csv);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Xslx csv, StringBuilder sb)
        {
            csv.csvData = sb.ToString();
            csv.inserttimetamp = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }


        public async Task<Xslx> GetXslx(int Id)
        {
            return await _context.Csv.FirstOrDefaultAsync(c => c.Id == Id);
        }
    }
}

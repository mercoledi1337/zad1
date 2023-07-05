using exercise1.Data;
using exercise1.Interfaces;
using exercise1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public Xslx GetXslx(int id)
        {
            return _context.Csv.Where(u => u.Id == id).FirstOrDefault();
        }

        public Task<IActionResult> UploadXslx(IFormFileCollection csv)
        {
            throw new NotImplementedException();
        }
    }
}

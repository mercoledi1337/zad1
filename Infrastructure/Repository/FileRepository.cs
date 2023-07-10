using exercise1.Data;
using exercise1.Interfaces;
using exercise1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace exercise1.Repository
{
    public class FileRepository : Interfaces.IFileUpload
    {
        private readonly DataContext _context;

        public FileRepository(DataContext context)
        {
            _context = context;

        }
        public async Task<Models.IFileUpload> Get(IFormFile csv)
        {
            return await _context.File.FirstOrDefaultAsync(w => w.Name == csv.FileName);
        }

        public async Task Save(Models.IFileUpload csv)
        {
            
            await _context.File.AddAsync(csv);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Models.IFileUpload csv, StringBuilder sb)
        {
            csv.Json = sb.ToString();
            csv.inserttimetamp = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }

        public async Task<Models.IFileUpload> GetFile(int Id)
        {
            return await _context.File.FirstOrDefaultAsync(c => c.Id == Id);
        }
    }
}

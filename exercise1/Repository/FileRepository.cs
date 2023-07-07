using ChoETL;
using exercise1.Data;
using exercise1.Interfaces;
using exercise1.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace exercise1.Repository
{
    public class FileRepository : IFileUpload
    {
        private readonly DataContext _context;

        public FileRepository(DataContext context)
        {
            _context = context;

        }
        public async Task<FileUpload> Get(IFormFile csv)
        {
            return await _context.File.FirstOrDefaultAsync(w => w.Name == csv.FileName);
        }

        public async Task Save(FileUpload csv)
        {
            
            await _context.File.AddAsync(csv);
            await _context.SaveChangesAsync();
        }

        public async Task Update(FileUpload csv, StringBuilder sb)
        {
            csv.Json = sb.ToString();
            csv.inserttimetamp = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }

        public async Task<FileUpload> GetFile(int Id)
        {
            return await _context.File.FirstOrDefaultAsync(c => c.Id == Id);
        }
    }
}

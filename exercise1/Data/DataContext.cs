using exercise1.Models;
using Microsoft.EntityFrameworkCore;

namespace exercise1.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<FileUpload> File { get; set; }
    }
}

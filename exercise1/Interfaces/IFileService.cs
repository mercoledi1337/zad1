using exercise1.Models;
using System.Text;

namespace exercise1.Interfaces
{
    public interface IFileService
    {
        public FileResultClass Get(int id); 
        public Task Upload(IFormFile name);
    }
}

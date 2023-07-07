using exercise1.Models;
using Microsoft.AspNetCore.Http;
using System.Text;

namespace exercise1.Interfaces
{
    public interface IFileUpload
    {
        public Task<FileUpload> GetFile(int Id);
        public Task<FileUpload> Get(IFormFile csv);
        public Task Save(FileUpload json);
        public Task Update(FileUpload json, StringBuilder sb);
    }
}

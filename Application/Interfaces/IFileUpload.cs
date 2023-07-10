using exercise1.Models;
using Microsoft.AspNetCore.Http;
using System.Text;

namespace exercise1.Interfaces
{
    public interface IFileUpload
    {
        public Task<Models.IFileUpload> GetFile(int Id);
        public Task<Models.IFileUpload> Get(IFormFile csv);
        public Task Save(Models.IFileUpload json);
        public Task Update(Models.IFileUpload json, StringBuilder sb);
    }
}

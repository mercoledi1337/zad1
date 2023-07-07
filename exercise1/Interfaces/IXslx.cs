using exercise1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace exercise1.Interfaces
{
    public interface IXslx
    {
        public Task<Xslx> GetXslx(int Id);
        public Task<Xslx> Get(IFormFile csv);
        public Task Save(Xslx csv);
        public Task Update(Task<Xslx> csv);
    }
}

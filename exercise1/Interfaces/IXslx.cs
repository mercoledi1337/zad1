using exercise1.Models;
using Microsoft.AspNetCore.Mvc;

namespace exercise1.Interfaces
{
    public interface IXslx
    {
        public XslxresultClass GetXslx(int Id);
        public Task<IActionResult> UploadXslx(IFormFileCollection csv);
    }
}

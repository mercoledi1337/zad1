using ChoETL;
using exercise1.Interfaces;
using exercise1.Models;
using exercise1.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Xml.Xsl;

namespace exercise1.Services
{
    public class XslxService : Controller
    {
        private readonly IXslx _xslxRepository;

        public XslxService(IXslx xslxRepository)
        {
            _xslxRepository = xslxRepository;
        }
        public XslxresultClass Get(int id)
        {
            var csv = _xslxRepository.GetXslx(id);
            StringBuilder tmp = new StringBuilder();
            using (var r = ChoJSONReader.LoadText(csv.csvData))
            {
                using (var w = new ChoCSVWriter(tmp).WithFirstLineHeader())
                {
                    w.Write(r);
                }
            }
            XslxresultClass result = new()
            {
                Id = csv.Id,
                Name = csv.Name,
                csvData = tmp
            };

            return result;
        }
    }
}

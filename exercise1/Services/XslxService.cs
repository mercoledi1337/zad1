using ChoETL;
using exercise1.Interfaces;
using exercise1.Models;
using exercise1.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Xml.Xsl;

namespace exercise1.Services
{
    public class XslxService : IXslxService
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
            using (var r = ChoJSONReader.LoadText(csv.Result.csvData))
            {
                using (var w = new ChoCSVWriter(tmp).WithFirstLineHeader())
                {
                    w.Write(r);
                }
            }
            XslxresultClass result = new()
            {
                Id = csv.Id,
                Name = csv.Result.Name,
                csvData = tmp
            };

            return result;
        }

        public async Task Upload(IFormFile csv)
        {
            StringBuilder sb = new StringBuilder();
            using (var p = new ChoCSVReader(csv.OpenReadStream())
                .WithFirstLineHeader()
                )
            {
                using (var w = new ChoJSONWriter(sb))
                    w.Write(p);
            }
            var tmpCsv = new Xslx
            {
                Name = csv.FileName,
                csvData = sb.ToString(),
                inserttimetamp = DateTime.UtcNow
            };
            var tmp = _xslxRepository.Get(csv);
            if (_xslxRepository.Get(csv) == null) 
                await _xslxRepository.Save(tmpCsv);
            else
                tmp.Result.csvData = sb.ToString();
                tmp.Result.inserttimetamp = DateTime.UtcNow;
                await _xslxRepository.Update(tmp, sb);




        }
    }
}

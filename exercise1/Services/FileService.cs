using ChoETL;
using exercise1.Interfaces;
using exercise1.Models;
using exercise1.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Xml.Xsl;

namespace exercise1.Services
{
    public class FileService : IFileService
    {
        private readonly IFileUpload _fileUpload;

        public FileService(IFileUpload fileUpload)
        {
            _fileUpload = fileUpload;
        }
        public FileResultClass Get(int id)
        {
            //taking from base and making csv
            var csv = _fileUpload.GetFile(id);
            StringBuilder tmp = new StringBuilder();
            using (var r = ChoJSONReader.LoadText(csv.Result.Json))
            {
                using (var w = new ChoCSVWriter(tmp).WithFirstLineHeader())
                {
                    w.Write(r);
                }
            }
            FileResultClass result = new()
            {
                Id = csv.Id,
                Name = csv.Result.Name,
                Json = tmp
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
            var tmpCsv = new FileUpload
            {
                Name = csv.FileName,
                Json = sb.ToString(),
                inserttimetamp = DateTime.UtcNow
            };
            var tmp = await _fileUpload.Get(csv);
            if (tmp == null)
            {
                await _fileUpload.Save(tmpCsv);
            }
            else
            {
                await _fileUpload.Update(tmp, sb);
            }



        }
    }
}

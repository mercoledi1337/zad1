using ChoETL;
using exercise1.Data;
using exercise1.Repository;
using exercise1.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace exercise1.Tests
{
    public class FileServiceIntegrationTest
    {
        private DataContext ArangeDb() 
        {
            DbContextOptions<DataContext> dbContextOptions = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: "dbo")
            .Options;
            DataContext context = new DataContext(dbContextOptions);
            context.Database.EnsureCreated();
            return context;
        }
        [Fact]
        public async Task GivenCsv_UploadCsvTo_ShouldBeOverrideWithSameNameCsv()
        {
            //Arange
            var context = ArangeDb();
            var stream = System.IO.File.OpenRead("test.csv");
            IFormFile tmpFile = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));
            var fileUpload = new FileRepository(context);
            var fileService = new FileService(fileUpload);

            //Act
            await fileService.Upload(tmpFile);
            await fileService.Upload(tmpFile);
            await fileService.Upload(tmpFile);

            //Asert
            var res = context.File.Count();
            res.Should().Be(1);
        }

        [Fact]
        public async Task GivenCsv_UploadCsvTo_ShouldBeAddedToDb()
        {
            //Arange
            var context = ArangeDb();
            var stream = System.IO.File.OpenRead("test.csv");
            IFormFile tmpFile = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));
            var fileUpload = new FileRepository(context);
            var fileService = new FileService(fileUpload);

            //Act
            await fileService.Upload(tmpFile);

            //Asert
            var res = context.File.Count();
            res.Should().Be(1); 
        }
    }
}
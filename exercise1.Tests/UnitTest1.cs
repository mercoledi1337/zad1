using ChoETL;
using exercise1.Data;
using exercise1.Repository;
using exercise1.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;



namespace exercise1.Tests
{
    public class UnitTest1
    {
        private static DbContextOptions<DataContext> dbContextOptions = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: "dbo")
            .Options;

        DataContext context;

        [Fact]
        [OneTimeSetUp]
        public async Task FileRepository_Update_OvverrideWithSameName()
        {
            //try to test when we put file
            context = new DataContext(dbContextOptions);
            context.Database.EnsureCreated();
            SeedDatabase();
            var stream = System.IO.File.OpenRead("test.csv");
            IFormFile tmpFile = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));
            var fileUpload = new FileRepository(context);
            var tmp = new FileService(fileUpload);
            await tmp.Upload(tmpFile);
            await tmp.Upload(tmpFile);
            await tmp.Upload(tmpFile);
            var res = context.File.Count();
            res.Should().NotBe(5);
            res.Should().Be(3);
        }

        public void CleanUp()
        {
            context.Database.EnsureDeleted();
        }

        private void SeedDatabase()
        {
            var tmp = new Models.IFileUpload()
            {
                Name = "test",
                Json = "asd",
                inserttimetamp = DateTime.UtcNow
            };
            var tmp1 = new Models.IFileUpload()
            {
                Name = "test1",
                Json = "asd1",
                inserttimetamp = DateTime.UtcNow
            };


            context.File.Add(tmp);
            context.SaveChanges();

            context.File.Add(tmp1);
            context.SaveChanges();



        }
    }
}
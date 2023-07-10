using ChoETL;
using exercise1.Data;
using exercise1.Interfaces;
using exercise1.Models;
using exercise1.Repository;
using exercise1.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.ObjectModel;
using Moq;
using NUnit.Framework;
using System.IO;
using System.Text;
using System.Xml.Linq;

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
            StringBuilder tmp = new StringBuilder("override");
            var fileRepo = new FileRepository(context);
            var t = context.File.FirstOrDefault(c => c.Name == "test");
            await fileRepo.Update(t, tmp);
            t.Json.Should().NotBeNull();
            t.Json.Should().Be("override");   
        }



        
        public void CleanUp()
        {
            context.Database.EnsureDeleted();
        }

        private void SeedDatabase()
        {
            
            var tmp = new FileUpload()
            {
                Name = "test",
                Json = "asd",
                inserttimetamp = DateTime.UtcNow
            };

            var tmp1 = new FileUpload()
            {
                Name = "test1",
                Json = "asd1",
                inserttimetamp = DateTime.UtcNow
            };
            
            context.File.AddAsync(tmp);
            context.SaveChanges();

            context.File.Add(tmp1);
            context.SaveChanges();

            
        }
            
    }
}
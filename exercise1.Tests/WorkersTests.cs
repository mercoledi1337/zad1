using Application.Interface;
using Application.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exercise1.Tests
{
    public class WorkersTests
    {
        [Fact]
        public async Task StringWithWorkerLevel_ReturnStringR9_WhenGivenStringBoss()
        {
            //Arrange
            var worker = "boss";
            var expectedBoss = "R9";

            //Act
            var service = new WorkersService();
            var boss = service.GetWorkerByLevel(worker);

            //Act
            Assert.True(expectedBoss == boss);
        }

        [Fact]
        public async Task tmp()
        {
            //Arrange
            var workerService = new Mock<IWorkerService>();
            workerService.Setup(x => x.Count()).Returns(2);
            int expecteAmoutOfWorkers = 2;

            //Act
            var service = new WorkersService();
            var countedAllWorkers = service.CountAllWorkers(workerService.Object);

            //Act
            Assert.True(countedAllWorkers == expecteAmoutOfWorkers);
        }
    }
}

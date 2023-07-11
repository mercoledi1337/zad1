using Application.Interface;
using Application.Services;
using Moq;

namespace exercise1.Tests
{
    public class WorkersUnitTests 
    {
        [Fact]
        public void GetWorker_BasedOnLevel_ReturnValidValue()
        
        {
            //Arrange
            var worker = "boss";
            var expectedBoss = "R9";

            //Act
            var service = new WorkersService();
            var boss = service.GetWorkerByLevel(worker);

            //Asert
            Assert.True(expectedBoss == boss);
        }

        [Fact]
        public void GetAmountOfWorker_BasedOnLevel_ReturnValidValue()
        {
            //Arrange
            var workerServiceMock = new Mock<IWorkerService>();
            workerServiceMock.Setup(x => x.Count("boss")).Returns(1);
            int expectedAmountOfWorkers = 1;

            //Act
            var service = new WorkersService();
            var countedWorkers = service.CountAllWorkersWithGivenLevel(workerServiceMock.Object, "boss");

            //Asertt
            Assert.True(countedWorkers == expectedAmountOfWorkers);
        }
    }
}

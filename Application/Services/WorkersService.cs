using Application.Interface;

namespace Application.Services
{
    public class WorkersService
    {
        public string GetWorkerByLevel(string worker)
        {
            if (worker == "boss")
                return "R9";
            else
                return "Messi";
        }

        public int CountAllWorkers(IWorkerService workerService, string boss)
        {
            return workerService.Count(boss);
        }


    }
}

using Bitlush;
using ScheduleTest.Models;


namespace ScheduleTest.Services.Interfaces
{
    internal interface IDataService
    {
        public Dictionary<int, List<TaskModel>> GenerateList();

    }
}

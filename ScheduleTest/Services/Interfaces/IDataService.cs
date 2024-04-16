using Bitlush;
using ScheduleTest.Models;


namespace ScheduleTest.Services.Interfaces
{
    internal interface IDataService
    {
        public AvlTree<int, TaskModel> GenerateRandomTree();

    }
}

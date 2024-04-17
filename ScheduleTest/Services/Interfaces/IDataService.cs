using Bitlush;
using ScheduleTest.Models;
using System.Collections.ObjectModel;


namespace ScheduleTest.Services.Interfaces
{
    internal interface IDataService
    {
        public int[] Counts
        {
            get;
        }

        public DateTime LineStartTime
        {
            get;
        }

        public DateTime LineDeadTime
        {
            get;
        }
        public Dictionary<int, ObservableCollection<TaskModel>> GenerateObservableCollection();

    }
}

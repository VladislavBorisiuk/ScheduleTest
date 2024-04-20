using ScheduleTest.Infrastructure.Extensions.Base;
using ScheduleTest.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleTest.Infrastructure.Extensions
{
    internal class TaskModelProvider : IItemsProvider<TaskModel>
    {
        private readonly List<TaskModel> _taskList;
        private readonly int _totalCount;

        public TaskModelProvider(List<TaskModel> taskList)
        {
            _taskList = taskList;
            _totalCount = _taskList.Count;
        }

        public int FetchCount()
        {
            return _totalCount;
        }

        public IList<TaskModel> FetchRange(int startIndex, int count)
        {
            return _taskList.Skip(startIndex).Take(count).ToList();
        }

        public void AddItem(TaskModel item) 
        {
            _taskList.Add(item);
        }

        public void RemoveItem(int id) 
        {
            _taskList.Remove(_taskList[id]);
        }


    }
}

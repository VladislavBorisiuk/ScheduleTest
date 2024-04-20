using Bitlush;
using ScheduleTest.Infrastructure.Extensions;
using ScheduleTest.Models;
using ScheduleTest.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleTest.Services
{
    internal class DataService : IDataService
    {

        private DateTime lineStartTime;
        public DateTime LineStartTime
        {
            get => lineStartTime;
            private set { lineStartTime = value; }
        }

        private DateTime lineDeadLine;
        public DateTime LineDeadTime
        {
            get => lineDeadLine;
            private set { lineDeadLine = value; }
        }

        private int[] counts;
        public int[] Counts
        {
            get => counts;
            private set { counts = value; }
        }

        public async Task<Dictionary<int, AsyncVirtualizingCollection<TaskModel>>> GenerateVirtualizingCollectionsAsync()
        {
            var collectionDictionary = new Dictionary<int, AsyncVirtualizingCollection<TaskModel>>();
            Counts = new int[3];
            var treeDictionary = GenerateRandomTrees();

            // Найти самое маленькое значение StartTime
            LineStartTime = treeDictionary
                .SelectMany(pair => pair.Value)
                .Select(node => node.Key)
                .Min();

            // Найти самое большое значение DeadLine
            LineDeadTime = treeDictionary
                .SelectMany(pair => pair.Value)
                .Select(node => node.Value.DeadLine)
                .Max();

            foreach (var kvp in treeDictionary)
            {
                var avlTree = kvp.Value;
                var itemsProvider = new TaskModelProvider(avlTree.Select(node => node.Value).ToList());
                var asyncVirtualizingCollection = new AsyncVirtualizingCollection<TaskModel>(itemsProvider,100,1000);
                collectionDictionary[kvp.Key] = asyncVirtualizingCollection;
            }

            return collectionDictionary;
        }

        private Dictionary<int, AvlTree<DateTime, TaskModel>> GenerateRandomTrees()
        {
            var treeDictionary = new Dictionary<int, AvlTree<DateTime, TaskModel>>();
            Random random = new Random();
            int count = random.Next(10000, 20000);
            for (int i = 0; i < count; i++)
            {

                DateTime today = DateTime.Today;
                DateTime startTime = today.AddDays(random.Next(-30, 30)).AddHours(random.Next(0, 12)).AddMinutes(random.Next(0, 60));
                DateTime deadLine = startTime.AddDays(random.Next(0, 20)).AddHours(random.Next(1, 12)).AddMinutes(random.Next(0, 60));

                TaskModel task = new TaskModel
                {
                    Name = "Task " + i.ToString(),
                    DeadLine = deadLine,
                    StartTime = startTime,
                    Type = GetRandomType(random),
                    Layer = random.Next(1, 2),
                    TaskSheduleNumber = random.Next(1, 6)
                };

                int layer = task.Layer;

                if (!treeDictionary.ContainsKey(layer))
                {
                    treeDictionary[layer] = new AvlTree<DateTime, TaskModel>();
                }

                AvlTree<DateTime, TaskModel> layerTree = treeDictionary[layer];

                layerTree.Insert(task.StartTime, task);

                switch (task.Type)
                {
                    case "Complete":
                        Counts[0]++;
                        break;
                    case "InProcess":
                        Counts[1]++;
                        break;
                    case "UnComplete":
                        Counts[2]++;
                        break;
                }
            }

            return treeDictionary;
        }

        private string GetRandomType(Random random)
        {
            string[] types = { "Complete", "InProcess", "UnComplete" };
            return types[random.Next(types.Length)];
        }
    }
}

﻿using Bitlush;
using ScheduleTest.Models;
using ScheduleTest.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ScheduleTest.Services
{
    internal class DataService : IDataService
    {
        private readonly Dictionary<int, ObservableCollection<TaskModel>> _layerTaskObservableCollections = new Dictionary<int, ObservableCollection<TaskModel>>();

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

        public Dictionary<int, ObservableCollection<TaskModel>> GenerateObservableCollection()
        {
            var listDictionary = new Dictionary<int, ObservableCollection<TaskModel>>();
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
                var taskObservableCollection = new ObservableCollection<TaskModel>(avlTree.Select(node => node.Value));
                listDictionary[kvp.Key] = taskObservableCollection;
            }
            return listDictionary;
        }

        private Dictionary<int, AvlTree<DateTime, TaskModel>> GenerateRandomTrees()
        {
            var treeDictionary = new Dictionary<int, AvlTree<DateTime, TaskModel>>();
            Random random = new Random();
            int count = random.Next(10, 10000);
            for (int i = 0; i < count; i++)
            {

                DateTime today = DateTime.Today;
                DateTime startTime = today.AddDays(random.Next(-10, 150)).AddHours(random.Next(0,24)); 
                DateTime deadLine = startTime.AddDays(random.Next(1, 30)).AddHours(random.Next(0, 24));

                TaskModel task = new TaskModel
                {
                    DeadLine = deadLine,
                    StartTime = startTime,
                    Type = GetRandomType(random),
                    Layer = random.Next(1, 10),
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

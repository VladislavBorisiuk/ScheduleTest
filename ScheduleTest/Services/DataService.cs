using Bitlush;
using ScheduleTest.Models;
using ScheduleTest.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ScheduleTest.Services
{
    internal class DataService : IDataService
    {
        private readonly Dictionary<int, List<TaskModel>> _layerTaskLists = new Dictionary<int, List<TaskModel>>();

        public Dictionary<int, List<TaskModel>> GenerateList()
        {
            var listDictionary = new Dictionary<int, List<TaskModel>>();
            var treeDictionary = GenerateRandomTrees();
            foreach (var kvp in treeDictionary)
            {
                var avlTree = kvp.Value;
                var taskList = avlTree.Select(node => node.Value).ToList();
                listDictionary[kvp.Key] = taskList;
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
                // Генерируем случайные даты для StartTime и DeadLine
                DateTime today = DateTime.Today;
                DateTime startTime = today.AddDays(random.Next(-1000, 1000)); // случайная дата в пределах последних 30 дней
                DateTime deadLine = startTime.AddDays(random.Next(1, 30)); // случайная дата в пределах следующих 30 дней

                TaskModel task = new TaskModel
                {
                    DeadLine = deadLine,
                    StartTime = startTime,
                    Type = GetRandomType(random),
                    Layer = random.Next(1, 50)
                };
                AvlTree<DateTime, TaskModel> layerTree = treeDictionary[task.Layer];
                InsertTaskToTree(task, layerTree);
            }

            return treeDictionary;
        }

        private bool InsertTaskToTree(TaskModel task, AvlTree<DateTime, TaskModel> tree)
        {
            foreach (AvlNode<DateTime, TaskModel> node in tree)
            {
                TaskModel existingTask = node.Value;

                if (task.StartTime <= existingTask.DeadLine && task.DeadLine >= existingTask.StartTime)
                {
                    // Если задача пересекается с существующей задачей в AVL-дереве, прерываем цикл
                    return false;
                }
            }

            // Если задача не пересекается с существующими задачами, вставляем ее в AVL-дерево
            tree.Insert(task.StartTime, task);
            return true;
        }

        private string GetRandomType(Random random)
        {
            string[] types = { "Complete", "InProcess", "UnComplete" };
            return types[random.Next(types.Length)];
        }
    }
}

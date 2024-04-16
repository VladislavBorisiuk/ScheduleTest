using Bitlush;
using ScheduleTest.Models;
using ScheduleTest.Services.Interfaces;


namespace ScheduleTest.Services
{
    internal class DataService : IDataService
    {
        private readonly AvlTree<int, TaskModel> _avlTree = new AvlTree<int, TaskModel>();

        public AvlTree<int, TaskModel> GenerateRandomTree()
        {
            Random random = new Random();
            int count = random.Next(10, 1000);
            HashSet<int> usedKeys = new HashSet<int>(); // Для отслеживания уже использованных ключей
            for (int i = 0; i < count; i++)
            {
                int key;
                // Генерируем уникальный ключ, который еще не использовался
                do
                {
                    key = random.Next(100000); // Можно выбрать нужный диапазон для ключей
                } while (usedKeys.Contains(key));

                // Генерируем случайные даты для StartTime и DeadLine
                DateTime today = DateTime.Today;
                DateTime startTime = today.AddDays(random.Next(-365, 365)); // случайная дата в пределах последних 30 дней
                DateTime deadLine = startTime.AddDays(random.Next(1, 30)); // случайная дата в пределах следующих 30 дней

                TaskModel task = new TaskModel
                {
                    Name = $"Task {i + 1}",
                    DeadLine = deadLine,
                    StartTime = startTime,
                    Type = GetRandomType(random),
                    Layer = random.Next(1, 10)
                };

                if (IsOverlapping(task))
                {
                    continue;
                }

                _avlTree.Insert(key, task);
                usedKeys.Add(key); // Добавляем использованный ключ в набор
            }
            int cot = CountNodes(_avlTree.Root);
            return _avlTree;
        }



        private string GetRandomType(Random random)
        {
            string[] types = { "Complete", "InProcess", "UnComplete" };
            return types[random.Next(types.Length)];
        }

        private int CountNodes(AvlNode<int, TaskModel> node)
        {
            if (node == null)
            {
                return 0;
            }

            // Рекурсивно подсчитываем количество узлов в левом и правом поддеревьях
            int leftCount = CountNodes(node.Left);
            int rightCount = CountNodes(node.Right);

            // Количество узлов в текущем поддереве равно сумме узлов в левом и правом поддеревьях плюс 1 (текущий узел)
            return leftCount + rightCount + 1;
        }



        private bool IsOverlapping(TaskModel task)
        {
            foreach (AvlNode<int, TaskModel> node in _avlTree)
            {
                TaskModel existingTask = node.Value;

                if (existingTask.Layer == task.Layer &&
                    (task.StartTime <= existingTask.DeadLine && task.DeadLine >= existingTask.StartTime))
                {
                    return true;
                }
            }
            return false;
        }

       
    }
}

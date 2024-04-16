using Bitlush;
using ScheduleTest.Models;
using ScheduleTest.Services.Interfaces;
using ScheduleTest.ViewModels.Base;
using System.Collections.ObjectModel;

namespace ScheduleTest.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        private readonly IUserDialog _UserDialog;
        private readonly IDataService _DataService;


        private AvlTree<int, TaskModel> _avlTree;

        public AvlTree<int, TaskModel> AvlTree
        {
            get => _avlTree;

            set => _avlTree = value;
        }

        private ObservableCollection<TaskModel> models;
        public ObservableCollection<TaskModel> Models
        {
            get => models;

            set => models = value;
        }
        #region Title : string - Заголовок окна

        /// <summary>Заголовок окна</summary>
        private string _Title = "Главное окно";

        /// <summary>Заголовок окна</summary>
        public string Title { get => _Title; set => Set(ref _Title, value); }

        #endregion

        #region Status : string - Статус

        /// <summary>Статус</summary>
        private string _Status = "Готов!";

        /// <summary>Статус</summary>
        public string Status { get => _Status; set => Set(ref _Status, value); }

        #endregion

        public MainWindowViewModel(IUserDialog UserDialog, IDataService DataService)
        {
            _UserDialog = UserDialog;
            _DataService = DataService;

            AvlTree = _DataService.GenerateRandomTree();

            int count = CountNodes(AvlTree.Root);

            ObservableCollection<TaskModel> collection = new ObservableCollection<TaskModel>();
            Traverse(AvlTree.Root, collection);
            Models = collection;
        }

        private static void Traverse(AvlNode<int, TaskModel> node, ObservableCollection<TaskModel> collection)
        {
            if (node != null)
            {
                Traverse(node.Left, collection);
                collection.Add(node.Value);
                Traverse(node.Right, collection);
            }
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

    }
}

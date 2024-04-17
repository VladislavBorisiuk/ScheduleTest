using Bitlush;
using ScheduleTest.Models;
using ScheduleTest.Services.Interfaces;
using ScheduleTest.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace ScheduleTest.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        private readonly IUserDialog _UserDialog;
        private readonly IDataService _DataService;

        #region Аттрибуты
        private Dictionary<int, ObservableCollection<TaskModel>> taskList;

        public Dictionary<int, ObservableCollection<TaskModel>> TaskList
        {
            get => taskList;

            set => taskList = value;
        }

        public DateTime StartTime
        {
            get => _DataService.LineStartTime;
        }

        public DateTime DeadTime
        {
            get => _DataService.LineDeadTime;
        }

        private int scheduleNumber = 4;

        public int ScheduleNumber
        {
            get => scheduleNumber;

            set
            {
                scheduleNumber = value;
                OnPropertyChanged(nameof(ScheduleNumber));
            }
        }

        private int[] counts ;
        
        public int[] Counts
        {
            get => counts;

            set => counts = value;
        }

        private ObservableCollection<TaskModel> models;
        public ObservableCollection<TaskModel> Models
        {
            get => models;

            set => models = value;
        }
        #endregion
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

            TaskList = _DataService.GenerateObservableCollection();

            Counts = _DataService.Counts;
            
        }
    }
}

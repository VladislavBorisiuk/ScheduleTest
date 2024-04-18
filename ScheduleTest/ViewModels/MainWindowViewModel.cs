using Bitlush;
using ScheduleTest.Infrastructure.Commands;
using ScheduleTest.Infrastructure.Commands.Base;
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

        #region Комманды
        public LambdaCommandAsync GenerateSchedule { get; }
        private bool CanGenerateScheduleAsyncExecute(object p) => true;

        private async Task GenerateScheduleAsyncExecute(object barabanName)
        {
            TaskList = await _DataService.GenerateObservableCollectionAsync();
            counts = _DataService.Counts;
            OnPropertyChanged(nameof(ScheduleNumber));
        }
        #endregion
        #region Аттрибуты
        private Dictionary<int, ObservableCollection<TaskModel>> taskList;

        public Dictionary<int, ObservableCollection<TaskModel>> TaskList
        {
            get => taskList;

            set
            {
                taskList= value;
                counts = _DataService.Counts;
                OnPropertyChanged(nameof(TaskList));
                OnPropertyChanged(nameof(CountsComplete));
                OnPropertyChanged(nameof(CountsInProc));
                OnPropertyChanged(nameof(CountsUnCom));
            }
        }

        public DateTime StartTime
        {
            get => _DataService.LineStartTime;
        }

        public DateTime DeadTime
        {
            get => _DataService.LineDeadTime;
        }

        private int scheduleNumber;

        public int ScheduleNumber
        {
            get => scheduleNumber;

            set
            {
                scheduleNumber = value;
                OnPropertyChanged(nameof(ScheduleNumber));
            }
        }

        private int[] counts;

        
        public string CountsComplete
        {
            get => counts[0].ToString() + " Complete";
            
        }

        public string CountsInProc
        {
            get => counts[1].ToString() + " Pending";
        }

        public string CountsUnCom
        {
            get => counts[2].ToString() + " Jeopardy";
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
            counts = new int[] {0, 0, 0};
            GenerateSchedule = new LambdaCommandAsync(GenerateScheduleAsyncExecute, CanGenerateScheduleAsyncExecute);
        }
    }
}

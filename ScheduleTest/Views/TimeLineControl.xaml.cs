using ScheduleTest.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ScheduleTest.Views
{
    public class TimeLineControl : Control
    {
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(Dictionary<int, ObservableCollection<TaskModel>>), typeof(TimeLineControl));

        public Dictionary<int, ObservableCollection<TaskModel>> ItemsSource
        {
            get { return (Dictionary<int, ObservableCollection<TaskModel>>)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public static readonly DependencyProperty StartTimeProperty =
            DependencyProperty.Register("LineStartTime", typeof(DateTime), typeof(TimeLineControl));

        public DateTime LineStartTime
        {
            get { return (DateTime)GetValue(StartTimeProperty); }
            set { SetValue(StartTimeProperty, value); }
        }

        public static readonly DependencyProperty DeadLineProperty =
            DependencyProperty.Register("LineDeadLine", typeof(DateTime), typeof(TimeLineControl));

        public DateTime LineDeadLine
        {
            get { return (DateTime)GetValue(DeadLineProperty); }
            set { SetValue(DeadLineProperty, value); }
        }


        public int ScheduleNumber
        {
            get { return (int)GetValue(ScheduleNumberProperty); }
            set { SetValue(ScheduleNumberProperty, value); }
        }

        public static readonly DependencyProperty ScheduleNumberProperty =
            DependencyProperty.Register("ScheduleNumber", typeof(int), typeof(TimeLineControl));
    }

}

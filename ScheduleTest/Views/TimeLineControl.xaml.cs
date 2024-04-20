using ScheduleTest.Infrastructure.Extensions;
using ScheduleTest.Models;
using System.Windows;
using System.Windows.Controls;

namespace ScheduleTest.Views
{
    public class TimeLineControl : Control
    {
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(Dictionary<int, AsyncVirtualizingCollection<TaskModel>>), typeof(TimeLineControl));

        public Dictionary<int, AsyncVirtualizingCollection<TaskModel>> ItemsSource
        {
            get { return (Dictionary<int, AsyncVirtualizingCollection<TaskModel>>)GetValue(ItemsSourceProperty); }
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

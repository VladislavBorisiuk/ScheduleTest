using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ScheduleTest.Infrastructure.Converters
{
    public class TaskSheduleNumberToIsEnabledConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values != null && values.Length == 2)
            {
                if (values[0] != DependencyProperty.UnsetValue && values[1] != DependencyProperty.UnsetValue)
                {
                    int timelineSheduleNumber = (int)values[0];
                    int taskSheduleNumber = (int)values[1];
                    if(timelineSheduleNumber == taskSheduleNumber)
                    {
                        return Visibility.Visible;
                    }
                }
                else
                {
                    return Visibility.Collapsed;
                }
            }
            return Visibility.Collapsed;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

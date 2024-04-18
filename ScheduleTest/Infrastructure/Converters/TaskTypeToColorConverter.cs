
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
namespace ScheduleTest.Infrastructure.Converters
{
    public class TaskTypeToColorConverter : IMultiValueConverter
    {
        private static readonly SolidColorBrush GreenBrush = new SolidColorBrush(Colors.Green);
        private static readonly SolidColorBrush YellowBrush = new SolidColorBrush(Colors.Yellow);
        private static readonly SolidColorBrush RedBrush = new SolidColorBrush(Colors.Red);
        private static readonly SolidColorBrush TransparentBrush = new SolidColorBrush(Colors.Transparent);

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values != null && values.Length == 3)
            {

                switch (values[0])
                {
                    case "Complete":
                        return ApplyTransparency(GreenBrush, (int)values[1], (int)values[2]);
                    case "InProcess":
                        return ApplyTransparency(YellowBrush, (int)values[1], (int)values[2]);
                    case "UnComplete":
                        return ApplyTransparency(RedBrush, (int)values[1], (int)values[2]);
                    default:
                        return ApplyTransparency(TransparentBrush, (int)values[1], (int)values[2]);
                }
            }
            return TransparentBrush;
        }

        private SolidColorBrush ApplyTransparency(SolidColorBrush brush, int cSh, int taskSh)
        {
            if (cSh == taskSh)
            {
                brush.Opacity = 1;
                return brush;
            }
            brush.Opacity = 0.4;
            return brush;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}


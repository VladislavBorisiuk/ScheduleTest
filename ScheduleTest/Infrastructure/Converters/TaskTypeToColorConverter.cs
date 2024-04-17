using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace ScheduleTest.Infrastructure.Converters
{
    public class TaskTypeToColorConverter : IValueConverter
    {
        // Создаем статические экземпляры кистей для каждого цвета
        private static readonly SolidColorBrush GreenBrush = new SolidColorBrush(Colors.Green);
        private static readonly SolidColorBrush YellowBrush = new SolidColorBrush(Colors.Yellow);
        private static readonly SolidColorBrush RedBrush = new SolidColorBrush(Colors.Red);
        private static readonly SolidColorBrush TransparentBrush = new SolidColorBrush(Colors.Transparent);

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string type)
            {
                // Возвращаем соответствующий кисть в зависимости от значения
                switch (type)
                {
                    case "Complete":
                        return GreenBrush;
                    case "InProcess":
                        return YellowBrush;
                    case "UnComplete":
                        return RedBrush;
                    default:
                        return TransparentBrush;
                }
            }
            return TransparentBrush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

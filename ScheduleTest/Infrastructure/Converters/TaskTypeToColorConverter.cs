
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace ScheduleTest.Infrastructure.Converters
{
        public class TaskTypeToColorConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                if (value is string type)
                {
                    switch (type)
                    {
                        case "Complete":
                            return Brushes.Green;
                        case "InProcess":
                            return Brushes.Yellow;
                        case "UnComplete":
                            return Brushes.Red;
                        default:
                            return Brushes.Transparent;
                    }
                }
                return Brushes.Transparent;
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }
}


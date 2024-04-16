using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ScheduleTest.Infrastructure.Converters
{
    internal class StartTimeToXPositionConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length < 2 || !(values[0] is DateTime) || !(values[1] is DateTime))
            {
                return 0;
            }

            DateTime startTime = (DateTime)values[0];
            DateTime endTime = (DateTime)values[1];

            TimeSpan duration = endTime - startTime;

            double minutesPerPixel = 60.0 / 24.0; // 24 часа на календаре
            double position = startTime.Hour * 60.0 * minutesPerPixel + startTime.Minute * minutesPerPixel ;

            return position;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

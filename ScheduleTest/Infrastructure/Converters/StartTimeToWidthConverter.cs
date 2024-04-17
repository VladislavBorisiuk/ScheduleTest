using System.Globalization;
using System.Windows.Data;

namespace ScheduleTest.Infrastructure.Converters
{
    public class StartTimeToWidthConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length < 2 || values[0] == null || values[1] == null)
                return null;

            DateTime lineStartTime = (DateTime)values[0];
            DateTime taskStartTime = (DateTime)values[1];

            // Вычисляем разницу в минутах между временем модели и временем линии
            double difference = (taskStartTime - lineStartTime).TotalMinutes * 0.85;

           
            // Возвращаем значение Left для Canvas
            return difference;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ScheduleTest.Views
{

    public class TimeLineInterfaceControl : Control
    {
        public static readonly DependencyProperty StartTimeProperty =
            DependencyProperty.Register("StartTime", typeof(DateTime), typeof(TimeLineInterfaceControl), new FrameworkPropertyMetadata(DateTime.Now));

        // Зависимое свойство для времени конца
        public static readonly DependencyProperty EndTimeProperty =
            DependencyProperty.Register("EndTime", typeof(DateTime), typeof(TimeLineInterfaceControl), new FrameworkPropertyMetadata(DateTime.Now.AddHours(1)));

        // Зависимое свойство для шага времени (в минутах)
        public static readonly DependencyProperty TimeStepProperty =
            DependencyProperty.Register("TimeStep", typeof(int), typeof(TimeLineInterfaceControl), new FrameworkPropertyMetadata(60));

        // Конструктор
        static TimeLineInterfaceControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TimeLineInterfaceControl), new FrameworkPropertyMetadata(typeof(TimeLineInterfaceControl)));
        }

        // Свойства доступа к зависимым свойствам
        public DateTime StartTime
        {
            get { return (DateTime)GetValue(StartTimeProperty); }
            set { SetValue(StartTimeProperty, value); }
        }

        public DateTime EndTime
        {
            get { return (DateTime)GetValue(EndTimeProperty); }
            set { SetValue(EndTimeProperty, value); }
        }

        public int TimeStep
        {
            get { return (int)GetValue(TimeStepProperty); }
            set { SetValue(TimeStepProperty, value); }
        }

        // Переопределение метода отрисовки
        // В методе OnRender класса TimeLineInterfaceControl
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            // Округляем StartTime до часов в меньшую сторону
            DateTime startTimeRounded = StartTime.Date.AddHours(StartTime.Hour);

            // Параметры для отрисовки штрихов времени
            Pen pen = new Pen(Brushes.Black, 1);
            double startX = 0;
            double endX = ActualWidth;
            double startY = ActualHeight / 2;
            double endY = ActualHeight / 2;

            // Расчет количества и расположения штрихов времени
            DateTime currentTime = startTimeRounded;
            while (currentTime <= EndTime)
            {
                double xPos = MapTimeToX(currentTime, startTimeRounded, EndTime, startX, endX);
                drawingContext.DrawLine(pen, new Point(xPos, startY - 5), new Point(xPos, startY + 5));
                FormattedText formattedText = new FormattedText(currentTime.ToString("HH:mm"), System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 12, Brushes.Black);
                drawingContext.DrawText(formattedText, new Point(xPos - formattedText.Width / 2, startY + 10));

                // Увеличение времени на шаг
                currentTime = currentTime.AddMinutes(TimeStep);
            }
        }


        // Метод для отображения времени в координате X
        private double MapTimeToX(DateTime time, DateTime startTime, DateTime endTime, double startX, double endX)
        {
            double totalMinutes = (endTime - startTime).TotalMinutes;
            double timePassed = (time - startTime).TotalMinutes*2.5;
            double fraction = timePassed / totalMinutes;
            return startX + (endX - startX) * fraction;
        }
    }
}



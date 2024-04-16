
namespace ScheduleTest.Models
{
    public class TaskModel
    {
        public string Name { get; set; }

        public DateTime DeadLine { get; set; }

        public DateTime StartTime { get; set; }

        public string Type { get; set; }

        public int Layer { get; set; }

        public double Wight => (DeadLine - StartTime).TotalMinutes / 900;
    }
}

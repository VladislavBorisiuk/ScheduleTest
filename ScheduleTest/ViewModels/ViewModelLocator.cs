using Microsoft.Extensions.DependencyInjection;

namespace ScheduleTest.ViewModels
{
    internal class ViewModelLocator
    {
        public MainWindowViewModel MainWindowModel => App.Services.GetRequiredService<MainWindowViewModel>();
    }
}

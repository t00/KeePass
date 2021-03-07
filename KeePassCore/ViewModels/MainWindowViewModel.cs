using System.Reactive;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using KeePassCore.Services;
using ReactiveUI;

namespace KeePassCore.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IClassicDesktopStyleApplicationLifetime desktop;
        private readonly OpenDatabaseService openDatabaseService;

        public MainWindowViewModel(IClassicDesktopStyleApplicationLifetime desktop, OpenDatabaseService openDatabaseService)
        {
            this.desktop = desktop;
            this.openDatabaseService = openDatabaseService;
            OpenCommand = ReactiveCommand.CreateFromTask(Open);
            ExitCommand = ReactiveCommand.CreateFromTask(Exit);
            
        }

        public ReactiveCommand<Unit, Unit> OpenCommand { get; }

        public ReactiveCommand<Unit, Unit> ExitCommand { get; set; }

        private async Task Open()
        {
            var dialog = new OpenFileDialog();
            var result = await dialog.ShowAsync(desktop.MainWindow);
            if (result != null)
            {
                await openDatabaseService.OpenFiles(result);
            }
        }
        
        private Task Exit()
        {
            desktop.Shutdown();
            return Task.CompletedTask;
        }
    }
}
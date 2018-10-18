using ProcessTree.ViewModels;
using System.Windows;

namespace ProcessTree
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var viewModel = new MainViewModel();
            var view = new MainWindow(viewModel);

            view.Show();
        }
    }
}
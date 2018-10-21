using ProcessTree.ViewModels;
using System.Windows;

namespace ProcessTree
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var treeManager = new ProcessTreeManager();
            var viewModel = new MainViewModel(treeManager);
            var view = new MainWindow(viewModel);

            view.Show();
        }
    }
}
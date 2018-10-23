using Ninject;
using Ninject.Extensions.Conventions;
using ProcessTree.ViewModels;
using System.Windows;

namespace ProcessTree
{
    public partial class App : Application
    {
        private StandardKernel CreateContainer()
        {
            var container = new StandardKernel();

            container.Bind(
                configurator => configurator
                .From("ProcessTree")
                .SelectAllClasses()
                .BindAllInterfaces()
                );

            return container;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var container = CreateContainer();
            MainWindow mainView = container.Get<MainWindow>();

            mainView.Show();
        }
    }
}
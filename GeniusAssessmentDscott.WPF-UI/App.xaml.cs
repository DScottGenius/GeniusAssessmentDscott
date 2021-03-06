using Autofac;
using GeniusAssessmentDscott.WPF_UI.Startup;
using System.Windows;

namespace GeniusAssessmentDscott.WPF_UI
{
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var bootstrapper = new Bootstrapper();
            var container = bootstrapper.Bootstrap();
            var mainWindow = container.Resolve<MainWindow>();
            mainWindow.Show();
        }
    }
}

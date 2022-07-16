using Autofac;
using GeniusAssessmentDscott.WPF_UI.Data;
using GeniusAssessmentDscott.WPF_UI.ViewModel;

namespace GeniusAssessmentDscott.WPF_UI.Startup
{
    public class Bootstrapper
    {
        public IContainer Bootstrap()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<MainWindow>().AsSelf();
            containerBuilder.RegisterType<MainViewModel>().AsSelf();
            containerBuilder.RegisterType<MainDataService>().As<IDataService>();

            return containerBuilder.Build();
        }
    }
}

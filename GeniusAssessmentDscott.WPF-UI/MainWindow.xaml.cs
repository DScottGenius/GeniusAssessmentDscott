using GeniusAssessmentDscott.WPF_UI.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace GeniusAssessmentDscott.WPF_UI
{

    public partial class MainWindow : Window
    {
        private MainViewModel viewModel;

        public MainWindow(MainViewModel viewModelIn)
        {
            InitializeComponent();
            //Get the view model
            viewModel = viewModelIn;

            //add it to the data context
            DataContext = viewModel;

            //Add a loaded event
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //Load the data
            viewModel.Load();
        }



    }
}

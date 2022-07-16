using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GeniusAssessmentDscott.WPF_UI.ViewModel
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string nameOfProperty = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameOfProperty));
        }

    }
}

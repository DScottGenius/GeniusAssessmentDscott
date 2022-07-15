using Microsoft.Extensions.Configuration;

namespace GeniusAssessmentDscott.Core.Commands
{
    public interface ICommand
    {
        void Execute();
        bool CanExecute();

        void OnFail();
    }
}

using GeniusAssessmentDscott.Core.Commands;

namespace GeniusAssessmentDscott.Logic
{
    public abstract class ILogic
    {
        protected string Filepath;
        protected ICommandManager commandManager;

        public abstract bool DidReadSucceed();
        public abstract void processData();
        protected abstract void UseDatabase();
    }
}
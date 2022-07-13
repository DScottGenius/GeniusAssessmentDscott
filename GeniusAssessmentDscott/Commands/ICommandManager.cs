namespace GeniusAssessmentDscott.Commands
{
    public interface ICommandManager
    {
        public abstract void InvokeCommand(ICommand commandIn);
    }
}
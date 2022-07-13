namespace GeniusAssessmentDscott.Commands
{
    public class CommandManager : ICommandManager
    {
        public void InvokeCommand(ICommand commandIn)
        {
            if (commandIn.CanExecute())
            {
                commandIn.Execute();
            }
            else
            {
                commandIn.OnFail();
            }
        }
    }
}

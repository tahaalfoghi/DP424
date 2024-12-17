namespace DP424.Web.Command
{
    //Invoker
    // Command Handler class act as Invoker
    // Command Pattern implementation
    public class CommandHandler
    {
        public async Task ExecuteCommand(ICommand command)
        {
            await command.Execute();
        }
    }
}

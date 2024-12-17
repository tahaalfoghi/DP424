namespace DP424.Web.Command
{
    // Command Pattern implementation
    public class CommandHandler
    {
        
        public async Task ExecuteCommand(ICommand command)
        {
            await command.Execute();
        }
    }


}

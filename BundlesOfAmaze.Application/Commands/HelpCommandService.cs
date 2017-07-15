using System.Threading.Tasks;

namespace BundlesOfAmaze.Application
{
    public class HelpCommandService : IHelpCommandService
    {
        public async Task<ResultMessage> HandleAsync(string commandPart)
        {
            return await Task.Run(() =>
            {
                ////.amazecats help
                ////.amazecats help create

                var message = "\n**Commands**\n";

                switch (commandPart)
                {
                    case Commands.Create:
                        message += "Usage: create [name] [gender:male|female]\n";
                        message += "Command to create a new cat\n";
                        message += "ex. create Kitty female\n";
                        break;

                    case Commands.Give:
                        message += "Usage: give [item name]\n";
                        message += "Command to feed or give an item to your cat\n";
                        message += "ex. give tuna\n";
                        break;

                    default:
                        message += "create - Creates a new cat. Use 'help create' for more information\n";
                        message += "give - Gives an item. Use 'help give' for more information\n";
                        message += "help - Shows this help\n";
                        break;
                }

                return new ResultMessage(message);
            });
        }
    }
}
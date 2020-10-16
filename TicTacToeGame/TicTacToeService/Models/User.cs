using TicTacToeService.Contracts;

namespace TicTacToeService.Models
{
    public class User
    {
        public string Nickname { get; set; }

        public ICallbackClient CallbackClient { get; set; }
    }
}
using ChatService.Contracts;

namespace ChatService.Models.ClientEntities
{
    internal class Client
    {
        public string Login { get; set; }

        public IChatCallback ChatCallback { get; set; }
    }
}
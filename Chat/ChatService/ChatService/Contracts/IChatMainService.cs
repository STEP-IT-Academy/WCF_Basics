using System.Collections.Generic;
using ChatService.Models.UserEntities;
using System.ServiceModel;

namespace ChatService.Contracts
{
    [ServiceContract(CallbackContract = typeof(IChatCallback))]
    public interface IChatMainService
    {
        [OperationContract]
        bool UserRegistration(string login, string password);

        [OperationContract]
        bool LoginPasswordValidation(string login, string password);

        [OperationContract(IsOneWay = true)]
        void UserLogin(string login);

        [OperationContract(IsOneWay = true)]
        void UserLogout();

        [OperationContract(IsOneWay = true)]
        void SendMessage(string message);

        [OperationContract(IsOneWay = true)]
        void SendMessageToClient(string login, string message);
    }

    public interface IChatCallback
    {
        [OperationContract(IsOneWay = true)]
        void DisplayMessageInChat(UserMessage message);

        [OperationContract(IsOneWay = true)]
        void GetAllConnectedUsers(IEnumerable<string> connectedUserLogins);

        [OperationContract(IsOneWay = true)]
        void UserNotification(string login, bool way);
    }
}
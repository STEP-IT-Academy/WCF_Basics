using ChatService.Contracts;
using ChatService.Models.ClientEntities;
using ChatService.Models.Context;
using ChatService.Models.UserEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace ChatService.Services
{
    public sealed class ChatMainService : IChatMainService
    {
        private static readonly List<Client> Clients = new List<Client>();
        private Client _currentClient;

        public bool UserRegistration(string login, string password)
        {
            using UserContext db = new UserContext();
            if (db.Users.Any(u => u.Login == login)) return false;
            db.Users.Add(new User {Login = login, Password = password});
            db.SaveChanges();
            return true;
        }

        public bool LoginPasswordValidation(string login, string password)
        {
            using UserContext db = new UserContext();
            return db.Users.Any(u => u.Login == login && u.Password == password);
        }

        public void UserLogin(string login)
        {
            IChatCallback callback = OperationContext.Current?.GetCallbackChannel<IChatCallback>();
            _currentClient = new Client {Login = login, ChatCallback = callback};
            Clients.Add(_currentClient);
            Clients.ForEach(c => c.ChatCallback.GetAllConnectedUsers(Clients.Select(c => c.Login)));
            foreach (var client in Clients)
            {
                client.ChatCallback.GetAllConnectedUsers(Clients.Select(c => c.Login));
                client.ChatCallback.UserNotification(login, true);
            }
        }

        public void UserLogout()
        {
            Clients.Remove(_currentClient);
            IChatCallback callback = OperationContext.Current?.GetCallbackChannel<IChatCallback>();
            foreach (var client in Clients)
            {
                client.ChatCallback.GetAllConnectedUsers(Clients.Select(c => c.Login));
                client.ChatCallback.UserNotification(_currentClient.Login, false);
            }
        }

        public void SendMessage(string message)
        {
            UserMessage userMessage = new UserMessage {Login = _currentClient.Login, DepartureTime = DateTime.Now, MessageText = message};
            Clients.ForEach(c => c.ChatCallback.DisplayMessageInChat(userMessage));
        }

        public void SendMessageToClient(string login, string message)
        {
            UserMessage userMessage = new UserMessage { Login = _currentClient.Login, DepartureTime = DateTime.Now, MessageText = message };
            Clients.Find(c => c.Login == login).ChatCallback.DisplayMessageInChat(userMessage);
        }
    }
}
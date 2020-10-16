using System.Collections.Generic;
using System.ServiceModel;
using TicTacToeService.Contracts;
using TicTacToeService.Models;

namespace TicTacToeService.Services
{
    public class TicTacToeMainService : ITicTacToeMainContract
    {
        private static List<User> _users = new List<User>();
        private User _user;
        private static int _moveCount = 0;
        private static readonly char[,] _gameMatrix =  
        {
            { '-', '-', '-' },
            { '-', '-', '-' },
            { '-', '-', '-' }
        };

        public int Join(string nickname, out string info)
        {
            ICallbackClient callbackClient = OperationContext.Current.GetCallbackChannel<ICallbackClient>();
            _user = new User {Nickname = nickname, CallbackClient = callbackClient};
            _users.Add(_user);

            if (_users.Count == 1)
            {
                info = "Вы играете крестиками. Ходите первым.";
                return 1;
            }
            else if(_users.Count == 2)
            {
                info = "Вы играете Ноликами. Ходите вторым.";
                return 2;
            }
            else
            {
                info = $"Вы - наблюдатель. Сейчас играют {_users[0].Nickname} и {_users[1].Nickname}";
                return 0;
            }
        }

        public void Leave()
        {
            _users.Remove(_user);
        }

        public string TryMove(int x, int y)
        {
            int userNumber = _users.IndexOf(_user);
            if (userNumber > 1)
            {
                return "Вы - наблюдатель.";
            }
            else if(_moveCount < 0)
            {
                return "Игра окончена.";
            }
            else if(_moveCount % 2 != userNumber)
            {
                return "Сейчас ходит другой игрок";
            }
            else if(_gameMatrix[x, y] != '-')
            {
                return "Ходить в эту клетку нельзя";
            }
            else
            {
                return null;
            }
        }

        private bool IsWinner(int x, int y, char ch)
        {

            int k = 0;
            for (int i = 0; i < _gameMatrix.GetLength(0); i++)
            {

                if (_gameMatrix[i, y] == ch) k++;
                else break;

            }

            if (k == _gameMatrix.GetLength(0)) return true;
            k = 0;
            for (int i = 0; i < _gameMatrix.GetLength(0); i++)
            {

                if (_gameMatrix[x, i] == ch) k++;
                else break;

            }

            if (k == _gameMatrix.GetLength(0)) return true;
            k = 0;

            if (x == y)
            {
                for (int i = 0; i < _gameMatrix.GetLength(0); i++)
                {

                    if (_gameMatrix[i, i] == ch) k++;
                    else break;

                }
                if (k == _gameMatrix.GetLength(0)) return true;
            }
            k = 0;

            if (x + y == _gameMatrix.GetLength(0) - 1)
            {
                for (int i = 0; i < _gameMatrix.GetLength(0); i++)
                {

                    if (_gameMatrix[i, _gameMatrix.GetLength(0) - 1 - i] == ch) k++;
                    else break;

                }
                if (k == _gameMatrix.GetLength(0)) return true;
            }
            return false;
        }

        public void SendMove(int x, int y)
        {
            string currentInfo = "";
            int userNumber = _users.IndexOf(_user);
            char currentSign = userNumber == 0 ? 'X' : ((userNumber == 1) ? '0' : '-');
            _gameMatrix[x, y] = currentSign;

            if (IsWinner(x, y, currentSign))
            {
                currentInfo = $"Победу одержал {_user.Nickname}";
                _moveCount -= 100;
            }

            _users.ForEach(u => u.CallbackClient.ShowMove(x, y, currentSign, currentInfo));
            _moveCount++;
        }

        public void ShowAllSteps()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (_gameMatrix[i, j] != '-')
                        _user.CallbackClient.ShowMove(i, j, _gameMatrix[i, j]);
                }
            }
        }
    }
}
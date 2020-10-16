using System.ServiceModel;

namespace TicTacToeService.Contracts
{
    [ServiceContract(CallbackContract = typeof(ICallbackClient), SessionMode = SessionMode.Required)]
    public interface ITicTacToeMainContract
    {
        [OperationContract]
        int Join(string nickname, out string info);

        [OperationContract(IsOneWay = true)]
        void Leave();

        [OperationContract]
        string TryMove(int x, int y);

        [OperationContract(IsOneWay = true)]
        void SendMove(int x, int y);

        [OperationContract(IsOneWay = true)]
        void ShowAllSteps();
    }

    public interface ICallbackClient
    {
        [OperationContract(IsOneWay = true)]
        void ShowMove(int x, int y, char sign, string info = "");
    }
}
using Akka.Actor;
using HomeWork.Client.Messages;
using HomeWork.Common.Messages;
using HomeWork.Infrastructure.Utilities;

namespace HomeWork.Client.Actors
{
    public class UserBalanceActor : ReceiveActor
    {
        private readonly IConsoleWriter _consoleWriter;
        private readonly IActorRef _userAccountsCoordinatorRemoteActor;

        public UserBalanceActor(IConsoleWriter consoleWriter, IActorRef userAccountsCoordinatorRemoteActor)
        {
            _consoleWriter = consoleWriter;
            _userAccountsCoordinatorRemoteActor = userAccountsCoordinatorRemoteActor;

            Receive<ChangeBalanceMessage>(message => RequestChangeBalance(message));
            Receive<BalanceReport>(message => HandleBalanceReport(message));
        }

        private void HandleBalanceReport(BalanceReport message)
        {
            _consoleWriter.WriteLine($"user {message.UserId} has balance '{message.CurrentBalance}' with last 5 operations: '{string.Join(", ",message.LastOperations)}'");
        }

        private void RequestChangeBalance(ChangeBalanceMessage message)
        {
            _consoleWriter.WriteLine($"User '{message.UserId}' requested change for amount '{message.ChangedAmount}'");

            _userAccountsCoordinatorRemoteActor.Tell(new ChangeBalance(message.UserId, message.ChangedAmount));
        }
    }
}
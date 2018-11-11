using System;
using Akka.Actor;
using HomeWork.Client.Messages;
using HomeWork.Common.Messages;

namespace HomeWork.Client.Actors
{
    public class UserBalanceActor : ReceiveActor
    {
        public UserBalanceActor()
        {
            Receive<ChangeBalanceMessage>(message => RequestChangeBalance(message));
            Receive<BalanceReport>(message => HandleBalanceReport(message));
        }

        private void HandleBalanceReport(BalanceReport message)
        {
            Console.WriteLine($"user {message.UserId} has balance '{message.CurrentBalance}' with last 5 operations: '{string.Join(", ",message.LastOperations)}'");
        }

        private void RequestChangeBalance(ChangeBalanceMessage message)
        {
            Console.WriteLine($"User '{message.UserId}' requested change for amount '{message.ChangedAmount}'");

            Context.ActorSelection("akka.tcp://ActorSystem@localhost:8091/user/UserAccountsCoordinator")
                .Tell(new ChangeBalance(message.UserId, message.ChangedAmount));
        }
    }
}
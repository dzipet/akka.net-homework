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
        }

        private void RequestChangeBalance(ChangeBalanceMessage message)
        {
            Console.WriteLine($"User '{message.UserId}' requested change for amount '{message.ChangedAmount}'");

            Context.ActorSelection("akka.tcp://ActorSystem@localhost:8091/user/UserAccountsCoordinator")
                .Tell(new ChangeUserAccountBalance(message.UserId, message.ChangedAmount));
        }
    }
}
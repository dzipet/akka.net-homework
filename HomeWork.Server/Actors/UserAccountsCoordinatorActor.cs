using System;
using System.Collections.Generic;
using Akka.Actor;
using HomeWork.Common.Messages;

namespace HomeWork.Server.Actors
{
    public class UserAccountsCoordinatorActor : ReceiveActor
    {
        private readonly Dictionary<Guid, IActorRef> _userAccounts;

        public UserAccountsCoordinatorActor()
        {
            _userAccounts = new Dictionary<Guid, IActorRef>();

            Receive<ChangeBalance>(message => HandleChangeUserAccountBalance(message));
        }

        private void HandleChangeUserAccountBalance(ChangeBalance message)
        {
            CreateChildUserIfNotExists(message.UserId);

            IActorRef childActorRef = _userAccounts[message.UserId];

            childActorRef.Tell(message, Sender);
        }

        private void CreateChildUserIfNotExists(Guid userId)
        {
            if (!_userAccounts.ContainsKey(userId))
            {
                IActorRef newChildActorRef = Context.ActorOf(Props.Create(() => new UserAccountActor(userId)), $"User{userId}");
                _userAccounts.Add(userId, newChildActorRef);

                Console.WriteLine($"UserAccountsCoordinatorActor created new child UserAccountActor for {userId} (Total Users: {_userAccounts.Count})");
            }
        }
    }
}
using System;
using System.Collections.Generic;
using Akka.Actor;
using HomeWork.Common.Messages;

namespace HomeWork.Server.Actors
{
    public class UserAccountActor : ReceiveActor
    {
        private List<ChangeUserAccountBalance> _events;
        private readonly Guid _userId;

        public UserAccountActor(Guid userId)
        {
            _userId = userId;
            _events = new List<ChangeUserAccountBalance>();

            Receive<ChangeUserAccountBalance>(message => HandleChangeUserAccountBalance(message));
        }

        private void HandleChangeUserAccountBalance(ChangeUserAccountBalance message)
        {
            
        }
    }
}
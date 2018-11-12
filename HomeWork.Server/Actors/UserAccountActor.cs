using System;
using System.Collections.Generic;
using System.Linq;
using Akka.Actor;
using HomeWork.Common.Messages;

namespace HomeWork.Server.Actors
{
    public class UserAccountActor : ReceiveActor
    {
        private List<ChangeBalance> _events;
        private readonly Guid _userId;

        public UserAccountActor(Guid userId, )
        {
            _userId = userId;
            _events = new List<ChangeBalance>();

            Receive<ChangeBalance>(message => HandleChangeUserAccountBalance(message));
        }

        private void HandleChangeUserAccountBalance(ChangeBalance message)
        {
            _events.Add(message);

            var balance = CalculateBalance();
            var lastOperations = GetLastOperations();
            var response = new BalanceReport(balance, lastOperations, message.UserId);

            Sender.Tell(response);
            Console.WriteLine($"{_userId} balance change operation executed");
        }

        private double CalculateBalance()
        {
            return _events.Sum(x => x.ChangedAmount);
        }

        private List<double> GetLastOperations()
        {
            return _events.TakeLast(5).Select(x => x.ChangedAmount).ToList();
        }
    }
}
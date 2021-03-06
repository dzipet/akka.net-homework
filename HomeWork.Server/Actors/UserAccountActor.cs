﻿using System;
using System.Collections.Generic;
using System.Linq;
using Akka.Actor;
using HomeWork.Common.Messages;
using HomeWork.Infrastructure.Utilities;

namespace HomeWork.Server.Actors
{
    public class UserAccountActor : ReceiveActor
    {
        private readonly List<ChangeBalance> _events;
        private readonly Guid _userId;
        private readonly IConsoleWriter _consoleWriter;

        public UserAccountActor(Guid userId, IConsoleWriter consoleWriter)
        {
            _userId = userId;
            _consoleWriter = consoleWriter;
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
            _consoleWriter.WriteLine($"{_userId} balance change operation executed");
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
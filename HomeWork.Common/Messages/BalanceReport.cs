using System;
using System.Collections.Generic;

namespace HomeWork.Common.Messages
{
    public class BalanceReport
    {
        public BalanceReport(double currentBalance, List<double> lastOperations, Guid userId)
        {
            CurrentBalance = currentBalance;
            LastOperations = lastOperations;
            UserId = userId;
        }

        public Guid UserId { get; }
        public double CurrentBalance { get; }
        public List<double> LastOperations { get; }
    }
}
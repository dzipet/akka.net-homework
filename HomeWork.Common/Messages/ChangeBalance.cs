using System;

namespace HomeWork.Common.Messages
{
    public class ChangeBalance
    {
        public ChangeBalance(Guid userId, double changedAmount)
        {
            UserId = userId;
            ChangedAmount = changedAmount;
        }

        public Guid UserId { get; }
        public double ChangedAmount { get; }
    }
}
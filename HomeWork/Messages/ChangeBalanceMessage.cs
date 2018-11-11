using System;

namespace HomeWork.Client.Messages
{
    public class ChangeBalanceMessage
    {
        public ChangeBalanceMessage(Guid userId, double changedAmount)
        {
            UserId = userId;
            ChangedAmount = changedAmount;
        }

        public Guid UserId { get; }
        public double ChangedAmount { get; }
    }
}
using System;

namespace HomeWork.Common.Messages
{
    public class ChangeUserAccountBalance
    {
        public ChangeUserAccountBalance(Guid userId, double changedAmount)
        {
            UserId = userId;
            ChangedAmount = changedAmount;
        }

        public Guid UserId { get; }
        public double ChangedAmount { get; }
    }
}
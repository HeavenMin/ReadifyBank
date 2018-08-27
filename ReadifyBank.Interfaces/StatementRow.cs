using System;
using ReadifyBank.Interfaces;

namespace ReadifyBank
{
    public class StatementRow : Interfaces.IStatementRow
    {
        private IAccount account;
        private DateTimeOffset date;
        private decimal amount;
        private decimal balance;
        private string description;

        public StatementRow(IAccount account, decimal amount, string description)
        {
            this.account = account;
            this.amount = amount;
            this.description = description;
            this.balance += amount;
            this.date = DateTimeOffset.Now.LocalDateTime;

        }
        
        public IAccount Account
        {
            get
            {
                return account;
            }
        }

        public DateTimeOffset Date
        {
            get
            {
                return date;
            }
        }
        public decimal Amount
        {
            get
            {
                return amount;
            }
        }

        public decimal Balance
        {
            get
            {
                return balance;
            }
        }

        public string Description
        {
            get
            {
                return description;
            }
        }
    }
}
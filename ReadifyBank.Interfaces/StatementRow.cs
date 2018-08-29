/*
  Author: Min Gao
  Date: 08/2018
  Versin: 1
  File Name: StatementRow.cs
  Purpose: Statement Row represents a single row in the bank statement
           It represents a single transaction on account
*/

using System;
using ReadifyBank.Interfaces;

namespace ReadifyBank
{
    class StatementRow : Interfaces.IStatementRow
    {
        private IAccount account;
        private DateTimeOffset date;    // Date and time of the transaction
        private decimal amount;
        private decimal balance;
        private string description;

        public StatementRow(IAccount account, decimal amount, string description)
        {
            this.account = account;
            this.amount = amount;
            this.description = description;
            this.balance = account.Balance;    // Balance after deposit or withdrawal
            this.date = DateTimeOffset.Now.LocalDateTime;

        }

        public StatementRow(IAccount account, decimal amount, string description, DateTimeOffset date)
        {
            this.account = account;
            this.amount = amount;
            this.description = description;
            this.balance = account.Balance;
            this.date = date;
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
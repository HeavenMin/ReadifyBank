/*
  Author: Min Gao
  Date: 08/2018
  Versin: 1
  File Name: Account.cs
  Purpose: Readify Bank Account
*/

using System;
using ReadifyBank.Interfaces;

namespace ReadifyBank
{
    class Account: Interfaces.IAccount
    {
        private DateTimeOffset openedDate;    //the date when the account was opened
        private string customerName;
        protected string accountNumber;
        private decimal balance;    // Current account balance

        public Account(string customerName)
        {
            this.customerName = customerName;
            this.balance = 0;
            this.openedDate = DateTimeOffset.Now.LocalDateTime;    //Local time when opening an account
        }

        public Account(string customerName, DateTimeOffset openedDate)
        {
            this.customerName = customerName;
            this.balance = 0;
            this.openedDate = openedDate;
        }

        public DateTimeOffset OpenedDate
        {
            get
            {
                return openedDate;
            }
        }

        public string CustomerName
        {
            get
            {
                return customerName;
            }
        }

        public decimal Balance
        {
            get
            {
                return balance;
            }
        }

        public string AccountNumber
        {
            get
            {
                return accountNumber;
            }
        }

        public void deposit(decimal amount)
        {
            this.balance += amount;
        }

        public void withdrawal(decimal amount)
        {
            if (this.balance >= amount)
            {
                this.balance -= amount;
            } else {
                throw new ArgumentException("Withdrawal amount exceeds balance!");
            }
        }
    }
}
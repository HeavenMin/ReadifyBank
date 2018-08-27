using System;
using ReadifyBank.Interfaces;

namespace ReadifyBank
{
    class Account: Interfaces.IAccount
    {
        private DateTimeOffset openedDate;
        private string customerName;
        protected string accountNumber;
        private decimal balance;

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
    }
}
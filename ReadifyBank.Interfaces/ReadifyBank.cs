/*
  Author: Min Gao
  Date: 08/2018
  Versin: 1
  File Name: Account.cs
  Purpose: Readify Bank method
*/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using ReadifyBank.Interfaces;

namespace ReadifyBank
{
    public class ReadifyBank : Interfaces.IReadifyBank
    {
        // The interest rate for Saving account is 6% monthly
        private const decimal SV_MONTHLY_INTEREST_RATE = 0.06m;
        // The interest rate for Home loan account is 3.99% annually
        private const decimal LN_ANNUAL_INTEREST_RATE = 0.0399m;

        private IList<IAccount> accountList;
        private IList<IStatementRow> transactionLog;

        public ReadifyBank()
        {
            this.accountList = new List<IAccount>();
            this.transactionLog = new List<IStatementRow>();
        }

        public IList<IAccount> AccountList
        {
            get
            {
                return accountList;
            }
        }

        public IList<IStatementRow> TransactionLog
        {
            get
            {
                return transactionLog;
            }
        }

        public IAccount OpenHomeLoanAccount(string customerName)
        {
            IAccount lnCustomer = new LNAccount(customerName);
            accountList.Add(lnCustomer);
            return lnCustomer;
        }

        public IAccount OpenHomeLoanAccount(string customerName, DateTimeOffset openDate)
        {
            IAccount lnCustomer = new LNAccount(customerName, openDate);
            accountList.Add(lnCustomer);
            return lnCustomer;

        }

        public IAccount OpenSavingsAccount(string customerName)
        {
            IAccount svCustomer = new SVAccount(customerName);
            accountList.Add(svCustomer);
            return svCustomer;
        }

        public IAccount OpenSavingsAccount(string customerName, DateTimeOffset openDate)
        {
            IAccount svCustomer = new SVAccount(customerName, openDate);
            accountList.Add(svCustomer);
            return svCustomer;
        }

        public void PerformDeposit(IAccount account, decimal amount, string description)
        {
            Account customerAccount = (Account) account;
            customerAccount.deposit(amount);
            StatementRow transaction = new StatementRow(account, amount, description);
            transactionLog.Add(transaction);
        }
        
        public void PerformDeposit(IAccount account, decimal amount, string description, DateTimeOffset depositDate)
        {
            Account customerAccount = (Account) account;
            customerAccount.deposit(amount);
            StatementRow transaction = new StatementRow(account, amount, description, depositDate);
            transactionLog.Add(transaction);
        }

        public void PerformWithdrawal(IAccount account, decimal amount, string description)
        {
            if (isBalanceEnough(account.Balance, amount))
            {
                Account customerAccount = (Account) account;
                customerAccount.withdrawal(amount);
            } else {
                Console.Error.WriteLine("Withdrawal failed.");
                return;
            }
            StatementRow transaction = new StatementRow(account, -amount, description);
            transactionLog.Add(transaction);
        }


        public void PerformWithdrawal(IAccount account, decimal amount, string description, DateTimeOffset withdrawalDate)
        {
            if (isBalanceEnough(account.Balance, amount))
            {
                Account customerAccount = (Account) account;
                customerAccount.withdrawal(amount);
            } else {
                Console.Error.WriteLine("Withdrawal failed.");
                return;
            }
            StatementRow transaction = new StatementRow(account, -amount, description, withdrawalDate);
            transactionLog.Add(transaction);
        }

        void PerformTransfer(IAccount from, IAccount to, decimal amount, string description)
        {
            if (isBalanceEnough(from.Balance, amount))
            {
                Account from_account = (Account) from;
                from_account.withdrawal(amount);
                Account to_account = (Account) to;
                to_account.deposit(amount);
            } else {
                Console.Error.WriteLine("Transfer failed.");
                return;
            }
            StatementRow from_transaction = new StatementRow(from, -amount, description);
            StatementRow to_transaction = new StatementRow(to, amount, description);
            transactionLog.Add(from_transaction);
            transactionLog.Add(to_transaction);
        }

        void PerformTransfer(IAccount from, IAccount to, decimal amount, string description, DateTimeOffset transferDate)
        {
            if (isBalanceEnough(from.Balance, amount))
            {
                Account from_account = (Account) from;
                from_account.withdrawal(amount);
                Account to_account = (Account) to;
                to_account.deposit(amount);
            } else {
                Console.Error.WriteLine("Transfer failed.");
                return;
            }
            StatementRow from_transaction = new StatementRow(from, -amount, description, transferDate);
            StatementRow to_transaction = new StatementRow(to, amount, description, transferDate);
            transactionLog.Add(from_transaction);
            transactionLog.Add(to_transaction);
        }

        public decimal GetBalance(IAccount account)
        {
            return account.Balance;
        }

        decimal CalculateInterestToDate(IAccount account, DateTimeOffset toDate)
        {

        }


        private bool isBalanceEnough(decimal balance, decimal amount)
        {
            if (balance < amount)
            {
                Console.Error.WriteLine("The balance is not enough.");
                return false;
            } else {
                return true;
            }
        }

    }
}
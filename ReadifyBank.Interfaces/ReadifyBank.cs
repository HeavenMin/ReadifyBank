/*
  Author: Min Gao
  Date: 08/2018
  Versin: 1
  File Name: ReadifyBank.cs
  Purpose: Readify Bank method
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using ReadifyBank.Interfaces;

namespace ReadifyBank
{
    public class ReadifyBank : Interfaces.IReadifyBank
    {
        // The interest rate for Saving account is 6% monthly
        private const decimal SV_MONTHLY_INTEREST_RATE = 0.06m;
        // The interest rate for Home loan account is 3.99% annually
        private const decimal LN_ANNUAL_INTEREST_RATE = 0.0399m;

        private IList<IAccount> accountList;    //bank accounts list
        private IList<IStatementRow> transactionLog;    //transactions log of bank

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
            if (isNameValid(customerName))
            {
                IAccount lnCustomer = new LNAccount(customerName);
                accountList.Add(lnCustomer);
                return lnCustomer;
            }
            return null;
        }

        public IAccount OpenHomeLoanAccount(string customerName, DateTimeOffset openDate)
        {
            if (isNameValid(customerName))
            {
                IAccount lnCustomer = new LNAccount(customerName, openDate);
                accountList.Add(lnCustomer);
                return lnCustomer;
            }
            return null;

        }

        public IAccount OpenSavingsAccount(string customerName)
        {
            if (isNameValid(customerName))
            {
                IAccount svCustomer = new SVAccount(customerName);
                accountList.Add(svCustomer);
                return svCustomer;
            }
            return null;
        }

        public IAccount OpenSavingsAccount(string customerName, DateTimeOffset openDate)
        {
            if (isNameValid(customerName))
            {
                IAccount svCustomer = new SVAccount(customerName, openDate);
                accountList.Add(svCustomer);
                return svCustomer;
            }
            return null;
        }

        public void PerformDeposit(IAccount account, decimal amount, string description)
        {
            if (!isAccountExistInSystem(account))
            {
                return;
            }
            if (amount > 0)
            {
                Account customerAccount = (Account) account;
                customerAccount.deposit(amount);
                StatementRow transaction = new StatementRow(account, amount, description);
                transactionLog.Add(transaction);
            } else {
                Console.Error.WriteLine("Amount less than or equal to 0. Invalid amount.");
            }
        }

        public void PerformDeposit(IAccount account, decimal amount, string description, DateTimeOffset depositDate)
        {
            if (!isAccountExistInSystem(account))
            {
                return;
            }
            if (amount > 0)
            {
                Account customerAccount = (Account) account;
                customerAccount.deposit(amount);
                StatementRow transaction = new StatementRow(account, amount, description, depositDate);
                transactionLog.Add(transaction);
            } else {
                Console.Error.WriteLine("Amount less than or equal to 0. Invalid amount.");
            }
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

        public void PerformTransfer(IAccount from, IAccount to, decimal amount, string description)
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

        public void PerformTransfer(IAccount from, IAccount to, decimal amount, string description, DateTimeOffset transferDate)
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

        //calculate interest for an account to a specific time
        public decimal CalculateInterestToDate(IAccount account, DateTimeOffset toDate)
        {
            DateTimeOffset today = DateTimeOffset.Now.Date;
            int interestDays = (toDate - today).Days;
            if (interestDays >= 1)
            {
                if (account.GetType() == typeof(LNAccount))
                {
                    return LN_ANNUAL_INTEREST_RATE / 365 * interestDays * account.Balance;
                } else if (account.GetType() == typeof(SVAccount))
                {
                    return SV_MONTHLY_INTEREST_RATE * 12 / 365 * interestDays * account.Balance;
                } else {
                    Console.Error.WriteLine("Invalid account.");
                    return 0m;
                }
            } else {
                Console.Error.WriteLine("Invalid date.");
                return 0m;
            }
        }

        //get mini statement (last 5 transactions occured one an account)
        public IEnumerable<IStatementRow> GetMiniStatement(IAccount account)
        {
            // TODO
            // return getAllTransactionsOfOneAccount(account).ToList().TakeLast(5);
            return getAllTransactionsOfOneAccount(account);
        }

        //close an account and return all transactions happended on the closed account
        public IEnumerable<IStatementRow> CloseAccount(IAccount account, DateTimeOffset closeDate)
        {
            if (account.Balance > 0)
            {
                string final_withdrawal_description = "Withdraw all the money before closing the account";
                PerformWithdrawal(account, account.Balance, final_withdrawal_description, closeDate);
            }
            IEnumerable<IStatementRow> all_transactions = getAllTransactionsOfOneAccount(account);
            accountList.Remove(account);
            //For the privacy and security of the customer, clear all the information of the customer after closing the account.
            account = null;
            return all_transactions;
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

        private IEnumerable<IStatementRow> getAllTransactionsOfOneAccount(IAccount account)
        {
            return transactionLog.Where(transaction => transaction.Account == account);
        }

        private bool isNameValid(string customerName)
        {
            Regex reg = new Regex(@"^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$");
            if (reg.Match(customerName).Success)
            {
                return true;
            } else {
                Console.Error.WriteLine("Invalid customer name.");
                return false;
            }
        }

        private bool isAccountExistInSystem(IAccount account)
        {
            if (accountList.Contains(account))
            {
                return true;
            } else {
                Console.Error.WriteLine("This account does not exist in our system.");
                return false;
            }
        }

    }
}

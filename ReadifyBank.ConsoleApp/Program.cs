/*
  Author: Min Gao
  Date: 08/2018
  Versin: 1
  File Name: Program.cs
  Purpose: demonstrate, have some fun.
*/

using System;
using System.Collections.Generic;
using ReadifyBank;
using ReadifyBank.Interfaces;
using System.Diagnostics;
using System.Linq;

namespace ReadifyBank.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ReadifyBank bank = new ReadifyBank();
            IAccount john = bank.OpenSavingsAccount("John");
            IAccount jack = bank.OpenHomeLoanAccount("Jack");
            foreach (int i in Enumerable.Range(1,9))
            {
                bank.OpenSavingsAccount("SCVustomer");
                bank.OpenHomeLoanAccount("LNCustomer");
            }
            Console.WriteLine(string.Format("After open 20 account. Total account number: {0}\n",
                                             bank.AccountList.Count));

            showCustomerInfo(john, bank);
            showCustomerInfo(jack, bank);
            foreach (int i in Enumerable.Range(1, 10))
            {
                bank.PerformDeposit(john, 100, "deposit 100");
                bank.PerformDeposit(jack, 50, "deposit 50");
            }
            Console.WriteLine("After deposit:");
            showCustomerInfo(john, bank);
            showCustomerInfo(jack, bank);

            foreach (int i in Enumerable.Range(1, 10))
            {
                bank.PerformWithdrawal(john, 20, "withdraw 20");
                bank.PerformWithdrawal(jack, 10, "withdraw 10");
            }
            Console.WriteLine("After withdrawal:");
            showCustomerInfo(john, bank);
            showCustomerInfo(jack, bank);

            bank.PerformTransfer(john, jack, 50, "lunch fee.");
            Console.WriteLine("After John transfer 50 to Jack:");
            showCustomerInfo(john, bank);
            showCustomerInfo(jack, bank);

            Console.WriteLine("When John transfor amount exceeds balance:");
            bank.PerformTransfer(john, jack, 800, "bill.");
            showCustomerInfo(john, bank);
            showCustomerInfo(jack, bank);

            Console.WriteLine("Calculate interest for 60 days for John and Jack:");
            DateTimeOffset toDate = DateTimeOffset.Now.LocalDateTime.AddDays(60);
            decimal interestForJohn = bank.CalculateInterestToDate(john, toDate);
            decimal interestForJack = bank.CalculateInterestToDate(jack, toDate);
            Console.WriteLine(string.Format("The interest for John to 60 days later is: {0:C4}", interestForJohn));
            Console.WriteLine(string.Format("The interest for Jack to 60 days later is: {0:C4}\n", interestForJack));

            IEnumerable<IStatementRow> miniStatementOfJohn = bank.GetMiniStatement(john);
            IEnumerable<IStatementRow> miniStatementOfJack = bank.GetMiniStatement(jack);
            Console.WriteLine("Mini statement of John:");
            foreach (IStatementRow transaction in miniStatementOfJohn)
            {
                showTransactionInfo(transaction, bank);
            }
            Console.WriteLine("\nMini statement of Jack:");
            foreach (IStatementRow transaction in miniStatementOfJack)
            {
                showTransactionInfo(transaction, bank);
            }

            Console.WriteLine("\nNow close the account John:");
            IEnumerable<IStatementRow> allTransactionOfJohn = bank.CloseAccount(john);
            foreach (IStatementRow transaction in allTransactionOfJohn)
            {
                showTransactionInfo(transaction, bank);
            }

        }

        private static void showCustomerInfo(IAccount account, ReadifyBank bank)
        {
            Console.WriteLine(string.Format("Customer {0} info:\nName: {0}, Account: {1}, " + 
                                            "Balance: {2}\nOpened date: {3}\n",
                                            account.CustomerName, account.AccountNumber, 
                                            account.Balance, account.OpenedDate));
        }

        private static void showTransactionInfo(IStatementRow transaction, ReadifyBank bank)
        {
            Console.WriteLine(string.Format("Transaction info: Account: {0}, Date: {1}, " + 
                                            "Amount: {2}, Balance: {3}, Description: {4}", 
                                            transaction.Account.AccountNumber, transaction.Date,
                                            transaction.Amount, transaction.Balance, 
                                            transaction.Description));
        }
    }
}

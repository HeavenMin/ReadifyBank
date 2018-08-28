/*
  Author: Min Gao
  Date: 08/2018
  Versin: 1
  File Name: Program.cs
  Purpose: demonstrate
*/

using System;
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
            Console.WriteLine(string.Format("Total account number: {0}\n", bank.AccountList.Count));
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
            Console.WriteLine("After john transfer 50 to jack:");
            showCustomerInfo(john, bank);
            showCustomerInfo(jack, bank);

            Console.WriteLine("John transfor amount exceeds balance:");
            bank.PerformTransfer(john, jack, 800, "bill.");
            showCustomerInfo(john, bank);
            showCustomerInfo(jack, bank);

        }

        private static void showCustomerInfo(IAccount account, ReadifyBank bank)
        {
            Console.WriteLine(string.Format("Customer {0} info:\nName: {0}\nAccount: {1}\nBalance: {2}\nOpened date: {3}\n",
                                             account.CustomerName, account.AccountNumber, account.Balance, account.OpenedDate));
        }
    }
}

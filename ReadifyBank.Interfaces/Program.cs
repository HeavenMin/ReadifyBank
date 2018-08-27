/*
  Author: Min Gao
  Date: 08/2018
  Versin: 1
  File Name: Account.cs
  Purpose: Test
*/

using System;
using ReadifyBank.Interfaces;
using System.Diagnostics;

namespace ReadifyBank
{
    class Program
    {
        static void Main(string[] args)
        {
            ReadifyBank bank = new ReadifyBank();
            IAccount john = bank.OpenSavingsAccount("John");
            IAccount jack = bank.OpenHomeLoanAccount("Jack");
            bank.PerformDeposit(john, 100, "deposit 100");
            Console.WriteLine(bank.CalculateInterestToDate(john, DateTimeOffset.Now.Date.AddDays(31)));

        }
    }
}
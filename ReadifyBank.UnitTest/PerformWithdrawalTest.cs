/*
  Author: Min Gao
  Date: 08/2018
  Versin: 1
  File Name: PerformWithdrawalTest.cs
  Purpose: unit test for PerformWithdrawal
*/

using System;
using System.Linq;
using ReadifyBank;
using ReadifyBank.Interfaces;
using Xunit;

namespace ReadifyBank.UnitTest
{
    public class PerformWithdrawalTest
    {
        [Fact]
        public void TestPerformWithdrawal()
        {
            ReadifyBank test_bank = new ReadifyBank();

            test_bank.PerformWithdrawal(null, 200, "withdrawwal 200.");
            Assert.Equal(0, test_bank.TransactionLog.Count);

            IAccount john = test_bank.OpenHomeLoanAccount("John");
            test_bank.PerformDeposit(john, 200, "deposit 200.");

            test_bank.PerformWithdrawal(john, 300, "withdrawal 300.");
            Assert.Equal(200, john.Balance);
            Assert.Equal(1, test_bank.TransactionLog.Count);

            test_bank.PerformWithdrawal(john, 100, "withdrawal 100.");
            Assert.Equal(100, john.Balance);
            Assert.Same(john, test_bank.TransactionLog.Last().Account);
            Assert.Equal(-100, test_bank.TransactionLog.Last().Amount);
            Assert.Equal(john.Balance, test_bank.TransactionLog.Last().Balance);
            Assert.Equal(2, test_bank.TransactionLog.Count);
        }
    }
}
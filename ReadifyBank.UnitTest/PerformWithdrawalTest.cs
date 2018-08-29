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
            ReadifyBank testBank = new ReadifyBank();

            testBank.PerformWithdrawal(null, 200, "withdrawwal 200.");
            Assert.Equal(0, testBank.TransactionLog.Count);

            IAccount john = testBank.OpenHomeLoanAccount("John");
            testBank.PerformDeposit(john, 200, "deposit 200.");

            testBank.PerformWithdrawal(john, 300, "withdrawal 300.");
            Assert.Equal(200, john.Balance);
            Assert.Equal(1, testBank.TransactionLog.Count);

            testBank.PerformWithdrawal(john, 100, "withdrawal 100.");
            Assert.Equal(100, john.Balance);
            Assert.Same(john, testBank.TransactionLog.Last().Account);
            Assert.Equal(-100, testBank.TransactionLog.Last().Amount);
            Assert.Equal(john.Balance, testBank.TransactionLog.Last().Balance);
            Assert.Equal(2, testBank.TransactionLog.Count);
        }
    }
}
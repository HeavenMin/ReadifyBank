/*
  Author: Min Gao
  Date: 08/2018
  Versin: 1
  File Name: PerformDepositTest.cs
  Purpose: unit test for PerformDeposit
*/

using System;
using System.Linq;
using ReadifyBank;
using ReadifyBank.Interfaces;
using Xunit;

namespace ReadifyBank.UnitTest
{
    public class PerformDepositTest
    {
        [Fact]
        public void TestPerformDeposit()
        {
            ReadifyBank test_bank = new ReadifyBank();

            test_bank.PerformDeposit(null, 200, "deposit 200.");
            Assert.Equal(0, test_bank.TransactionLog.Count);

            IAccount john = test_bank.OpenHomeLoanAccount("John");
            test_bank.PerformDeposit(john, -200, "invalid deposit.");
            Assert.Equal(0, john.Balance);
            Assert.Equal(0, test_bank.TransactionLog.Count);

            test_bank.PerformDeposit(john, 200, "deposit 200.");
            Assert.Equal(200, john.Balance);
            Assert.Same(john, test_bank.TransactionLog.Last().Account);
            Assert.Equal(200, test_bank.TransactionLog.Last().Amount);
            Assert.Equal(john.Balance, test_bank.TransactionLog.Last().Balance);
            Assert.Equal(1, test_bank.TransactionLog.Count);
        }
    }
}
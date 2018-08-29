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
            ReadifyBank testBank = new ReadifyBank();

            testBank.PerformDeposit(null, 200, "deposit 200.");
            Assert.Equal(0, testBank.TransactionLog.Count);

            IAccount john = testBank.OpenHomeLoanAccount("John");
            testBank.PerformDeposit(john, -200, "invalid deposit.");
            Assert.Equal(0, john.Balance);
            Assert.Equal(0, testBank.TransactionLog.Count);

            testBank.PerformDeposit(john, 200, "deposit 200.");
            Assert.Equal(200, john.Balance);
            Assert.Same(john, testBank.TransactionLog.Last().Account);
            Assert.Equal(200, testBank.TransactionLog.Last().Amount);
            Assert.Equal(john.Balance, testBank.TransactionLog.Last().Balance);
            Assert.Equal(1, testBank.TransactionLog.Count);
        }
    }
}
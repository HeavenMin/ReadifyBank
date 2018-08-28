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
            decimal johnBlanceBeforeDeposit = john.Balance;
            test_bank.PerformDeposit(john, -200, "invalid deposit.");
            decimal johnBalanceAfterDeposit = john.Balance;
            Assert.Equal(johnBlanceBeforeDeposit, johnBalanceAfterDeposit);

            test_bank.PerformDeposit(john, 200, "deposit 200.");
            johnBalanceAfterDeposit = john.Balance;
            Assert.Equal(johnBlanceBeforeDeposit + 200, johnBalanceAfterDeposit);
        }
    }
}
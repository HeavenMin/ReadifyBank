/*
  Author: Min Gao
  Date: 08/2018
  Versin: 1
  File Name: PerformTransferTest.cs
  Purpose: unit test for PerformTransfer
*/

using System;
using System.Linq;
using ReadifyBank;
using ReadifyBank.Interfaces;
using Xunit;

namespace ReadifyBank.UnitTest
{
    public class PerformTransferTest
    {
        [Fact]
        public void TestPerformTransfer()
        {
            ReadifyBank test_bank = new ReadifyBank();
            IAccount john = test_bank.OpenHomeLoanAccount("John");
            IAccount jack = test_bank.OpenSavingsAccount("Jack");
            test_bank.PerformDeposit(john, 200, "deposit 200.");

            test_bank.PerformTransfer(john, null, 100, "Tranfer 100.");
            Assert.Equal(200, john.Balance);
            Assert.Equal(1, test_bank.TransactionLog.Count);

            test_bank.PerformTransfer(null, john, 100, "Tranfer 100");
            Assert.Equal(200, john.Balance);
            Assert.Equal(1, test_bank.TransactionLog.Count);

            test_bank.PerformTransfer(john, jack, 100, "Tranfer 100 to jack.");
            Assert.Equal(100, john.Balance);
            Assert.Equal(100, jack.Balance);
            Assert.Equal(3, test_bank.TransactionLog.Count);
        }
    }
}
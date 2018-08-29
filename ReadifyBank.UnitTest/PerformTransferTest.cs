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
            ReadifyBank testBank = new ReadifyBank();
            IAccount john = testBank.OpenHomeLoanAccount("John");
            IAccount jack = testBank.OpenSavingsAccount("Jack");
            testBank.PerformDeposit(john, 200, "deposit 200.");

            // Test for invalid recipient account
            testBank.PerformTransfer(john, null, 100, "Tranfer 100.");
            Assert.Equal(200, john.Balance);
            Assert.Equal(1, testBank.TransactionLog.Count);

            // Test for invlaid payer account
            testBank.PerformTransfer(null, john, 100, "Tranfer 100");
            Assert.Equal(200, john.Balance);
            Assert.Equal(1, testBank.TransactionLog.Count);

            // Test for nomal circumstances
            testBank.PerformTransfer(john, jack, 100, "Tranfer 100 to jack.");
            Assert.Equal(100, john.Balance);
            Assert.Equal(100, jack.Balance);
            Assert.Equal(3, testBank.TransactionLog.Count);
        }
    }
}
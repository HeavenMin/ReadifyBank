/*
  Author: Min Gao
  Date: 08/2018
  Versin: 1
  File Name: OpenSavingAccountTest.cs
  Purpose: unit test for OpenSavingAccount
*/

using System;
using System.Linq;
using ReadifyBank;
using ReadifyBank.Interfaces;
using Xunit;

namespace ReadifyBank.UnitTest
{
    public class OpenSavingAccountTest
    {
        [Fact]
        public void TestOpenSavingAccount()
        {
            ReadifyBank testBank = new ReadifyBank();

            // Test for invalid customer name
            IAccount invalidName = testBank.OpenSavingsAccount("John123");
            Assert.Null(invalidName);
            Assert.Empty(testBank.AccountList);

            // Test for valid saving account customer
            IAccount john = testBank.OpenSavingsAccount("John");
            Assert.Equal("John", john.CustomerName);
            Assert.Equal(0, john.Balance);
            Assert.StartsWith("SV-", john.AccountNumber);
            Assert.Matches("^SV-\\d{6}$", john.AccountNumber);
            Assert.NotEmpty(testBank.AccountList);
            Assert.Equal(1, testBank.AccountList.Count);

            // Add another 19 saving accont
            foreach (int i in Enumerable.Range(1,19))
            {
                testBank.OpenSavingsAccount("SVCustomer", DateTimeOffset.Now.LocalDateTime);
            }
            Assert.Equal(20, testBank.AccountList.Count);
        }
    }
}
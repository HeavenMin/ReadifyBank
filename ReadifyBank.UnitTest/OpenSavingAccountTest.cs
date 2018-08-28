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
            ReadifyBank test_bank = new ReadifyBank();

            IAccount invalidName = test_bank.OpenSavingsAccount("John123");
            Assert.Null(invalidName);
            Assert.Empty(test_bank.AccountList);

            IAccount john = test_bank.OpenSavingsAccount("John");
            Assert.Equal("John", john.CustomerName);
            Assert.Equal(0, john.Balance);
            Assert.StartsWith("SV-", john.AccountNumber);
            Assert.Matches("^SV-\\d{6}$", john.AccountNumber);
            Assert.NotEmpty(test_bank.AccountList);
            Assert.Equal(1, test_bank.AccountList.Count);

            //add another 19 saving accont
            foreach (int i in Enumerable.Range(1,19))
            {
                test_bank.OpenSavingsAccount("SVCustomer", DateTimeOffset.Now.LocalDateTime);
            }
            Assert.Equal(20, test_bank.AccountList.Count);
        }
    }
}
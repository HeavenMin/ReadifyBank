/*
  Author: Min Gao
  Date: 08/2018
  Versin: 1
  File Name: OpenHomeLoanAccountTest.cs
  Purpose: unit test for OpenHomeLoanAccount
*/


using System;
using System.Linq;
using ReadifyBank;
using ReadifyBank.Interfaces;
using Xunit;

namespace ReadifyBank.UnitTest
{
    public class OpenHomeLoanAccountTest
    {
        [Fact]
        public void TestOpenHomeLoanAccount()
        {
            ReadifyBank test_bank = new ReadifyBank();
            IAccount john = test_bank.OpenHomeLoanAccount("John");
            Assert.Equal("John", john.CustomerName);
            Assert.Equal(0, john.Balance);
            Assert.StartsWith("LN-", john.AccountNumber);
            Assert.Matches("^LN-\\d{6}$", john.AccountNumber);
            Assert.NotEmpty(test_bank.AccountList);
            Assert.Equal(1, test_bank.AccountList.Count);

            //add another 19 saving accont
            foreach (int i in Enumerable.Range(1,19))
            {
                test_bank.OpenSavingsAccount(string.Format("SCVustomer{0}", i));
            }
            Assert.Equal(20, test_bank.AccountList.Count);
        }
    }
}
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
            ReadifyBank testBank = new ReadifyBank();

            // Test for invalid customer name
            IAccount invalidName = testBank.OpenHomeLoanAccount("John123");
            Assert.Null(invalidName);
            Assert.Empty(testBank.AccountList);

            // Test for valid home loan account customer
            IAccount john = testBank.OpenHomeLoanAccount("John");
            Assert.Equal("John", john.CustomerName);
            Assert.Equal(0, john.Balance);
            Assert.StartsWith("LN-", john.AccountNumber);
            Assert.Matches("^LN-\\d{6}$", john.AccountNumber);
            Assert.NotEmpty(testBank.AccountList);
            Assert.Equal(1, testBank.AccountList.Count);

            // Add another 19 home loan accont
            foreach (int i in Enumerable.Range(1,19))
            {
                testBank.OpenHomeLoanAccount("LNCustomer", DateTimeOffset.Now.LocalDateTime);
            }
            Assert.Equal(20, testBank.AccountList.Count);
        }
    }
}
/*
  Author: Min Gao
  Date: 08/2018
  Versin: 1
  File Name: CloseAccountTest.cs
  Purpose: unit test for CloseAccount
*/

using System;
using System.Linq;
using ReadifyBank;
using ReadifyBank.Interfaces;
using System.Collections.Generic;
using Xunit;

namespace ReadifyBank.UnitTest
{
    public class CloseAccountTest
    {
        [Fact]
        public void TestCloseAccount()
        {
            ReadifyBank test_bank = new ReadifyBank();
            IAccount john = test_bank.OpenHomeLoanAccount("John");
            IAccount jack = test_bank.OpenSavingsAccount("jack");
            test_bank.PerformDeposit(john, 100, "deposit 100.");

            IEnumerable<IStatementRow> allJackTransactions =  test_bank.CloseAccount(john);
            Assert.Null(john);
            Assert.Equal(1, test_bank.AccountList.Count);
            Assert.Equal(100, test_bank.TransactionLog.Last().Amount);
            Assert.Equal(0, test_bank.TransactionLog.Last().Balance);
            foreach (IStatementRow transaction in allJackTransactions)
            {
                Assert.Same(john, transaction.Account);
            }

        }
    }
}
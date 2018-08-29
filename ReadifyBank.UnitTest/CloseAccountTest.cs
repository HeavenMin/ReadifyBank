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
            ReadifyBank testBank = new ReadifyBank();
            IAccount john = testBank.OpenHomeLoanAccount("John");
            IAccount jack = testBank.OpenSavingsAccount("jack");
            foreach (int i in Enumerable.Range(1,20))
            {
                decimal depositAmount = (decimal) (i * 100);
                testBank.PerformDeposit(john, depositAmount, string.Format("deposit {0}.", depositAmount));
                testBank.PerformDeposit(jack, depositAmount, string.Format("deposit {0}.", depositAmount));
            }

            IEnumerable<IStatementRow> allJackTransactions =  testBank.CloseAccount(john);
            Assert.Equal(1, testBank.AccountList.Count);
            Assert.Equal(-21000, testBank.TransactionLog.Last().Amount);
            Assert.Equal(0, testBank.TransactionLog.Last().Balance);
            foreach (IStatementRow transaction in allJackTransactions)
            {
                Assert.Same(john, transaction.Account);
            }

        }
    }
}
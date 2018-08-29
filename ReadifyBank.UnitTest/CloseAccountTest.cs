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
            foreach (int i in Enumerable.Range(1,20))
            {
                decimal depositAmount = (decimal) (i * 100);
                test_bank.PerformDeposit(john, depositAmount, string.Format("deposit {0}.", depositAmount));
                test_bank.PerformDeposit(jack, depositAmount, string.Format("deposit {0}.", depositAmount));
            }

            IEnumerable<IStatementRow> allJackTransactions =  test_bank.CloseAccount(john);
            Assert.Equal(1, test_bank.AccountList.Count);
            Assert.Equal(-21000, test_bank.TransactionLog.Last().Amount);
            Assert.Equal(0, test_bank.TransactionLog.Last().Balance);
            foreach (IStatementRow transaction in allJackTransactions)
            {
                Assert.Same(john, transaction.Account);
            }

        }
    }
}
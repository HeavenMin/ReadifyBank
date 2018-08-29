/*
  Author: Min Gao
  Date: 08/2018
  Versin: 1
  File Name: GetMiniStatementTest.cs
  Purpose: unit test for GetMiniStatement
*/

using System;
using System.Linq;
using ReadifyBank;
using ReadifyBank.Interfaces;
using System.Collections.Generic;
using Xunit;

namespace ReadifyBank.UnitTest
{
    public class GetMiniStatementTest
    {
        [Fact]
        public void TestGetMiniStatement()
        {
            ReadifyBank testBank = new ReadifyBank();

            // Test for an account only have one transaction
            IAccount jill = testBank.OpenHomeLoanAccount("Jill");
            testBank.PerformDeposit(jill, 100, "deposit 100.");
            IEnumerable<IStatementRow> miniStatementForJill = testBank.GetMiniStatement(jill);
            Assert.Equal(1, miniStatementForJill.Count());
            Assert.Equal(100, miniStatementForJill.ToList()[0].Balance);

            // Test normal circumstances
            IAccount john = testBank.OpenHomeLoanAccount("John");
            IAccount jack = testBank.OpenSavingsAccount("Jack");
            foreach (int i in Enumerable.Range(1,20))
            {
                decimal depositAmount = (decimal) (i * 100);
                testBank.PerformDeposit(john, depositAmount, string.Format("deposit {0}.", depositAmount));
                testBank.PerformDeposit(jack, depositAmount, string.Format("deposit {0}.", depositAmount));
            }
            IEnumerable<IStatementRow> miniStatementForJohn = testBank.GetMiniStatement(john);
            Assert.Equal(5, miniStatementForJohn.Count());
            decimal johnBalance = 12000;
            foreach (int i in Enumerable.Range(0, 5))
            {
                Assert.Same(john, miniStatementForJohn.ToList()[i].Account);
                decimal amount = (16 + i) * 100;
                johnBalance += amount;
                Assert.Equal(amount , miniStatementForJohn.ToList()[i].Amount);
                Assert.Equal(johnBalance, miniStatementForJohn.ToList()[i].Balance);
            }

            // Test for invalid account
            IEnumerable<IStatementRow> miniStatementForNotExist = testBank.GetMiniStatement(null);
            Assert.Null(miniStatementForNotExist);

        }
    }
}
/*
  Author: Min Gao
  Date: 08/2018
  Versin: 1
  File Name: CalculateInterestToDateTest.cs
  Purpose: unit test for CalculateInterestToDate
*/

using System;
using System.Linq;
using ReadifyBank;
using ReadifyBank.Interfaces;
using Xunit;

namespace ReadifyBank.UnitTest
{
    public class CalculateInterestToDateTest
    {
        [Fact]
        public void TestCalculateInterestToDate()
        {
            ReadifyBank test_bank = new ReadifyBank();
            IAccount john = test_bank.OpenHomeLoanAccount("John");
            test_bank.PerformDeposit(john, 1000, "deposit 1000.");
            IAccount jack = test_bank.OpenSavingsAccount("jack");
            test_bank.PerformDeposit(jack, 1000, "deposit 1000.");

            DateTimeOffset invalidToDate = DateTimeOffset.Now.LocalDateTime.AddDays(-60);
            decimal interestForJohn = test_bank.CalculateInterestToDate(john, invalidToDate);
            decimal interestForJack = test_bank.CalculateInterestToDate(jack, invalidToDate);
            Assert.Equal(0m, interestForJohn);
            Assert.Equal(0m, interestForJack);

            const int ADD_DAYS = 60;
            DateTimeOffset toDate = DateTimeOffset.Now.LocalDateTime.AddDays(ADD_DAYS);
            interestForJohn = test_bank.CalculateInterestToDate(john, toDate);
            decimal LNInterestRate = interestForJohn * 365 / ADD_DAYS / 1000;
            Assert.True(LNInterestRate - 0.0399m < 0.0001m);
            interestForJack = test_bank.CalculateInterestToDate(jack, toDate);
            decimal SVInterestRate = interestForJack * 365 / 12 / ADD_DAYS / 1000;
            Assert.True(SVInterestRate - 0.06m < 0.0001m);

            decimal interestForNull = test_bank.CalculateInterestToDate(null, toDate);
            Assert.Equal(0m, interestForNull);
        }
    }
}
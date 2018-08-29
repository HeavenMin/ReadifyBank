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
            ReadifyBank testBank = new ReadifyBank();
            IAccount john = testBank.OpenHomeLoanAccount("John");
            testBank.PerformDeposit(john, 1000, "deposit 1000.");
            IAccount jack = testBank.OpenSavingsAccount("jack");
            testBank.PerformDeposit(jack, 1000, "deposit 1000.");

            // Test for invalid toDate
            DateTimeOffset invalidToDate = DateTimeOffset.Now.LocalDateTime.AddDays(-60);
            decimal interestForJohn = testBank.CalculateInterestToDate(john, invalidToDate);
            decimal interestForJack = testBank.CalculateInterestToDate(jack, invalidToDate);
            Assert.Equal(-1, interestForJohn);
            Assert.Equal(-1, interestForJack);

            // Test for valid toDate
            const int ADD_DAYS = 60;
            DateTimeOffset toDate = DateTimeOffset.Now.LocalDateTime.AddDays(ADD_DAYS);
            interestForJohn = testBank.CalculateInterestToDate(john, toDate);
            decimal LNInterestRate = interestForJohn * 365 / ADD_DAYS / 1000;
            Assert.True(LNInterestRate - 0.0399m < 0.0001m);
            interestForJack = testBank.CalculateInterestToDate(jack, toDate);
            decimal SVInterestRate = interestForJack * 365 / 12 / ADD_DAYS / 1000;
            Assert.True(SVInterestRate - 0.06m < 0.0001m);

            // Test for invalid account
            decimal interestForNull = testBank.CalculateInterestToDate(null, toDate);
            Assert.Equal(-1, interestForNull);
        }
    }
}
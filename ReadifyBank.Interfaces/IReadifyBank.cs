using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ReadifyBank.Interfaces
{
    /// <summary>
    /// Readify Bank interface
    /// </summary>
    public interface IReadifyBank
    {
        /// <summary>
        /// Bank accounts list
        /// </summary>
        IList<IAccount> AccountList { get; }
        
        /// <summary>
        /// Transactions log of the bank
        /// </summary>
        IList<IStatementRow> TransactionLog { get; }

        /// <summary>
        /// Open a home loan account
        /// </summary>
        /// <param name="customerName">Customer name</param>
        /// <param name="openDate">The date of the transaction</param>
        /// <returns>Opened Account</returns>
        IAccount OpenHomeLoanAccount(string customerName, DateTimeOffset openDate);

        /// <summary>
        /// Open a saving account
        /// </summary>
        /// <param name="customerName">Customer name</param>
        /// <param name="openDate">The date of the transaction</param>
        /// <returns>Opened account</returns>
        IAccount OpenSavingsAccount(string customerName, DateTimeOffset openDate);

        /// <summary>
        /// Deposit amount in an account
        /// </summary>
        /// <param name="account">Account</param>
        /// <param name="amount">Deposit amount</param>
        /// <param name="description">Description of the transaction</param>
        /// <param name="depositDate">The date of the transaction</param>
        void PerformDeposit(IAccount account, decimal amount, string description, DateTimeOffset depositDate);

        /// <summary>
        /// Withdraw amount in an account
        /// </summary>
        /// <param name="account">Account</param>
        /// <param name="amount">Withdrawal amount</param>
        /// <param name="description">Description of the transaction</param>
        /// <param name="withdrawalDate">The date of the transaction</param>
        void PerformWithdrawal(IAccount account, decimal amount, string description, DateTimeOffset withdrawalDate);

        /// <summary>
        /// Transfer amount from an account to an account
        /// </summary>
        /// <param name="from">From account</param>
        /// <param name="to">To account</param>
        /// <param name="amount">Transfer amount</param>
        /// <param name="description">Description of the transaction</param>
        /// <param name="transferDate">The date of the transaction</param>
        void PerformTransfer(IAccount from, IAccount to, decimal amount, string description, DateTimeOffset transferDate);

        /// <summary>
        /// Return the balance for an account
        /// </summary>
        /// <param name="account">Customer account</param>
        /// <returns></returns>
        decimal GetBalance(IAccount account);

        /// <summary>
        /// Calculate interest rate for an account to a specific time
        /// The interest rate for Saving account is 6% monthly
        /// The interest rate for Home loan account is 3.99% annually
        /// </summary>
        /// <param name="account">Customer account</param>
        /// <param name="toDate">Calculate interest to this date</param>
        /// <returns>The added value</returns>
        decimal CalculateInterestToDate(IAccount account, DateTimeOffset toDate);

        /// <summary>
        /// Get mini statement (the last 5 transactions occurred on an account)
        /// </summary>
        /// <param name="account">Customer account</param>
        /// <returns>Last five transactions</returns>
        IEnumerable<IStatementRow> GetMiniStatement(IAccount account);

        /// <summary>
        /// Close an account
        /// </summary>
        /// <param name="account">Customer account</param>
        /// <param name="closeDate">Close Date</param>
        /// <returns>All transactions happened on the closed account</returns>
        IEnumerable<IStatementRow> CloseAccount(IAccount account, DateTimeOffset closeDate);
    }
}

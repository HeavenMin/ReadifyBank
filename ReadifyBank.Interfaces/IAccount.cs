using System;

namespace ReadifyBank.Interfaces
{
    /// <summary>
    /// Readify Bank IAccount Interface
    /// There are two types of account : Home Loan and Saving
    /// </summary>
    public interface IAccount
    {
        /// <summary>
        /// The date when the account was opened
        /// </summary>
        DateTimeOffset OpenedDate { get; }

        /// <summary>
        /// Customer Name
        /// </summary>
        string CustomerName { get; }

        /// <summary>
        /// Account number
        /// It is formatted as follows: 2 characters for Account type,
        /// dash and 6 digits for account number starting from 1
        /// For home loan account it should start from "LN-000001"
        /// For saving account it should start from "SV-000001"
        /// </summary>
        string AccountNumber { get; }

        /// <summary>
        /// Current account balance
        /// </summary>
        decimal Balance { get; }
    }
}

/*
  Author: Min Gao
  Date: 08/2018
  Versin: 1
  File Name: SVAccount.cs
  Purpose: Readify Bank Saving Account
*/

using System;

namespace ReadifyBank
{
    class SVAccount : Account
    {
        private static Int32 svAccountNumberFactory = 0;
        public SVAccount(string customerName) : base(customerName)
        {
            // Start from "SV-000001"
            // If there may be more than 999999 customer in the future,
            // expending the range of account numbers can be considered at the beginning of the design.
            base.accountNumber = "SV-" + SVAccount.getUniqueSVaccountNumber.ToString("D6");
        }

        public SVAccount(string customerName, DateTimeOffset openedDate) : base(customerName, openedDate)
        {
            base.accountNumber = "SV-" + SVAccount.getUniqueSVaccountNumber.ToString("D6");
        }

        private static Int32 getUniqueSVaccountNumber
        {
            get
            {
                svAccountNumberFactory += 1;
                return svAccountNumberFactory;
            }
        }
    }
}
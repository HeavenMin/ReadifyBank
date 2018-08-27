using System;

namespace ReadifyBank
{
    //Home Loan Account
    class LNAccount : Account
    {
        private static Int32 lnAccountNumberFactory = 0;
        public LNAccount(string customerName) : base(customerName)
        {
            // Start from "LN-000001"
            base.accountNumber = "LN-" + LNAccount.getUniqueLNAccountNumber.ToString("D6");
        }

        public LNAccount(string customerName, DateTimeOffset openedDate) : base(customerName, openedDate)
        {
            base.accountNumber = "LN-" + LNAccount.getUniqueLNAccountNumber.ToString("D6");
        }

        private static Int32 getUniqueLNAccountNumber
        {
            get
            {
                lnAccountNumberFactory += 1;
                return lnAccountNumberFactory;
            }
        }
    }
}
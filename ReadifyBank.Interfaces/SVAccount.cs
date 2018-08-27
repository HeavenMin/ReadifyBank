using System;

namespace ReadifyBank
{
    // Saving Account
    class SVAccount : Account
    {
        private static Int32 svAccountNumberFactory = 0;
        public SVAccount(string customerName) : base(customerName)
        {
            // Start from "SV-000001"
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
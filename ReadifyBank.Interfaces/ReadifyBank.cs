using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using ReadifyBank.Interfaces;

namespace ReadifyBank
{
    public class ReadifyBank : Interfaces.IReadifyBank
    {
        private IList<IAccount> accountList;
        private IList<IStatementRow> transactionLog;

        public ReadifyBank()
        {
            this.accountList = new List<IAccount>();
            this.transactionLog = new List<IStatementRow>();
        }

        public IList<IAccount> AccountList
        {
            get
            {
                return accountList;
            }
        }

        public IList<IStatementRow> TransactionLog
        {
            get
            {
                return transactionLog;
            }
        }


    }
}
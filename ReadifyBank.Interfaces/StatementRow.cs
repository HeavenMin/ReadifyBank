using System;
using ReadifyBank.Interfaces;

namespace ReadifyBank
{
    public class StatementRow : Interfaces.IStatementRow
    {
        private IAccount account;
        private DateTimeOffset date;
        private decimal amount;
        private decimal balance;
        private string description;
        
        public IAccount Account => throw new NotImplementedException();

        public DateTimeOffset Date => throw new NotImplementedException();

        public decimal Amount => throw new NotImplementedException();

        public decimal Balance => throw new NotImplementedException();

        public string Description => throw new NotImplementedException();
    }
}
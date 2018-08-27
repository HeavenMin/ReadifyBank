using System;
using ReadifyBank.Interfaces;

namespace ReadifyBank
{
    class Program
    {
        static void Main(string[] args)
        {
            Account testAccout = new LNAccount("John");
            Console.WriteLine(testAccout.AccountNumber);
            Account testAccout2 = new LNAccount("John");
            Console.WriteLine(testAccout2.AccountNumber);
            Account testAccout3 = new LNAccount("John");
            Console.WriteLine(testAccout3.AccountNumber);
            Console.WriteLine(testAccout3.CustomerName);
            Console.WriteLine(testAccout3.Balance);
            Console.WriteLine(testAccout3.OpenedDate);
            IAccount svAccount1 = new SVAccount("Jack");
            Console.WriteLine(svAccount1.AccountNumber);

        }
    }
}
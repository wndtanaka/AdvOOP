using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inheritance.Scripts;

namespace Inheritance
{
    class Program
    {
        //public static string statement;
        static void Main(string[] args)
        {
            /*
             BankAccount savings = new SavingsAccount();

            Console.WriteLine("1. Generate Statement\n2. Deposit\n3. Withdraw");
            int userChoice = Convert.ToInt32(Console.ReadLine());

            if (userChoice == 1)
            {
                statement = savings.GetStatement();
                Console.WriteLine(statement);
            }
            else if (userChoice == 2)
            {
                Console.WriteLine("How much do you want to deposit?");
                int depositAmount = Convert.ToInt32(Console.ReadLine());
                savings.Deposit(depositAmount);
                statement = savings.GetStatement();
                Console.WriteLine(statement);
            }
            else if (userChoice == 3)
            {
                Console.WriteLine("How much do you want to withdraw?");
                int withdrawAmount = Convert.ToInt32(Console.ReadLine());
                savings.Withdraw(withdrawAmount);
                statement = savings.GetStatement();
                Console.WriteLine(statement);
                
            }
            */
            List<BankAccount> accounts = new List<BankAccount>();
            accounts.Add(new SavingsAccount());
            accounts.Add(new CheckingAccount());

            accounts[0].Deposit(10);
            accounts[1].Deposit(1000);

            accounts[1].Withdraw(1);

            foreach (var acc in accounts)
            {
                Console.WriteLine(acc.GetStatement());
            }
            Console.ReadLine();
        }
    }
}

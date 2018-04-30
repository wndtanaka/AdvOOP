using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance.Scripts
{
    class BankAccount
    {
        public int accountNumber;
        protected float money;

        public BankAccount()
        {
            Random rand = new Random(DateTime.Now.Millisecond);
            accountNumber = rand.Next(100000000, 999999999);
        }
        public virtual float Withdraw(float amount)
        {
            if (money < amount)
            {
                Console.WriteLine("You only have $" + money + " in your account");
                return 0f;
            }
            money -= amount;
            return amount;
        }
        public virtual void Deposit(float amount)
        {
            money += amount;
        }
        // Forms a statement and returns a string
        // containing said statement
        public virtual string GetStatement()
        {
            return GetAccountNo() + "\n\t" + GetMoney() + "\n";  
        }
        protected string GetAccountNo()
        {
            return "Account No.: " + accountNumber.ToString();
        }
        protected string GetMoney()
        {
            return "Money: $" + money.ToString();
        }
    }
}

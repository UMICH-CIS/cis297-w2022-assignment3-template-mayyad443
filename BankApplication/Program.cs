using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AccountHierarchy
{
    class Account
    {
        private decimal balance;

        public Account(decimal bal)
        {
            balance = bal;
        }

        public decimal Balance
        {
            get
            {
                return balance;
            }
            set
            {
                if (Balance >= 0)
                    balance = Balance;
                else
                    throw new Exception("Invalid Amount");
            }
        }

        public virtual bool Credit(decimal amt)
        {
            if (amt <= 0)
            {
                Console.WriteLine("Invalid amount to deposit!");
                return false;
            }
            else
            {
                balance += amt;
                return true;
            }

        }

        public virtual bool Debit(decimal amt)
        {
            if (amt <= 0)
            {
                Console.WriteLine("Invalid amount to withdraw!");
                return false;
            }
            else if (amt > balance)
            {
                Console.WriteLine("Debit amount exceeded account balance!");
                return false;
            }
            else
            {
                balance -= amt;
                return true;
            }
        }

    }
}
namespace AccountHierarchy
{
    class SavingsAccount : Account
    {
        private decimal interestRate;

        public SavingsAccount(decimal bal, decimal rate) : base(bal)
        {
            interestRate = rate;
        }

        public decimal CalculateInterest()
        {
            decimal interest = interestRate * base.Balance / 100;
            base.Credit(interest);
            return interest;
        }


    }
}
namespace AccountHierarchy
{
    class CheckingAccount : Account
    {
        private decimal feePerTransaction;

        public CheckingAccount(decimal bal, decimal fee) : base(bal)
        {
            feePerTransaction = fee;
        }

        public override bool Credit(decimal bal)
        {
            bool check = base.Credit(bal);
            if (check)
                base.Debit(feePerTransaction);
            return check;
        }

        public override bool Debit(decimal bal)
        {
            bool check = base.Debit(bal);
            if (check)
                base.Debit(feePerTransaction);
            return check;
        }
    }
}
namespace AccountHierarchy
{
    class AccountDriver
    {
        static void Mainzz(string[] args)
        {
            // creating a saving account with initial balance as $500 and intreset rate as 5%
            SavingsAccount savingsAcc = new SavingsAccount(500, 5);
            Console.WriteLine("Account Balance for Savings Account is: ${0}", savingsAcc.Balance);
            Console.WriteLine("Interest earned on the balance: ${0}", savingsAcc.CalculateInterest());
            Console.WriteLine("Depositing amount $250");
            savingsAcc.Credit(250);
            Console.WriteLine("Final Account balance is: ${0}", savingsAcc.Balance);
            Console.WriteLine("Withdrawing amount $100");
            savingsAcc.Debit(100);
            Console.WriteLine("Final Account balance is: ${0}", savingsAcc.Balance);
            Console.WriteLine();

            // creating a checking account with initial balance $1000 and fee per transaction as $10
            CheckingAccount checkingAcc = new CheckingAccount(1000, 10);
            Console.WriteLine("Account Balance for Checking Account is: ${0}", checkingAcc.Balance);
            Console.WriteLine("Depositing amount $300");
            checkingAcc.Credit(300);
            Console.WriteLine("Final Account balance is: ${0}", checkingAcc.Balance);
            Console.WriteLine("Withdrawing amount $200");
            checkingAcc.Debit(200);
            Console.WriteLine("Final Account balance is: ${0}", checkingAcc.Balance);
            Console.WriteLine();
            Console.ReadKey();

        }
    }
}

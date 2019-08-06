using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class TransactionSession
    {
        public decimal Balance { get; private set; }

        public TransactionSession()
        {
            Balance = 0.00M;
        }

        public bool Deposit(decimal amount)
        {
            bool isValidBill = false;

            if ((amount == 1) || (amount == 2) || (amount == 5) || (amount == 10))
            {
                isValidBill = true;
                Balance += amount;
            }

            return isValidBill;
        }

        public void Deduct(decimal amount)
        {
            Balance -= amount;
        }

        public string FinishTransaction()
        {
            int quarters = (int)(Balance / 0.25M);
            Balance -= quarters * 0.25M;

            int dimes = (int)(Balance / 0.10M);
            Balance -= dimes * 0.10M;

            int nickles = (int)(Balance / 0.05M);
            Balance -= nickles * 0.05M;

            decimal totalBalance = (quarters * 0.25M) + (dimes * 0.10M) + (nickles * 0.05M);

            // Run to see if it works
            return "Quarters: " + quarters + "\nDimes: " + dimes + "\nNickles: " + nickles + "\nTotal change: $" + totalBalance; 
                
        }

    }
}

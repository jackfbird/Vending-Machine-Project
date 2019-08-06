using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Capstone.Classes
{
    public class UserInterface
    {
        private VendingMachine vendingMachine = new VendingMachine();

        public void RunInterface()
        {
            vendingMachine.Initialize();
            Welcome();

            bool done = false;
            while (!done)
            {
                // --- method for Main Menu ---
                done = MainMenu();
            }
        }

        public bool MainMenu()
        {
            //Console.WriteLine(" *** WELCOME! *** ");
            Console.WriteLine("(1) Display Vending Machine Items");
            Console.Beep(600, 100);
            Thread.Sleep(200);
            Console.WriteLine("(2) Purchase");
            Console.Beep(700, 100);
            Thread.Sleep(200);
            Console.WriteLine("(3) End");
            Console.Beep(800, 100);
            Thread.Sleep(200);
            Console.WriteLine();
            Console.Write("Selection: ");
            string mainInput = Console.ReadLine();



            bool validMainInput = false;

            while (!validMainInput)
            {
                switch (mainInput)
                {
                    case "1":
                    case "one":
                        // Display Vending Machine Items
                        Display();
                        validMainInput = true;
                        break;

                    case "2":
                        //  Purchase
                        Console.Clear();
                        TransactionMenu();
                        validMainInput = true;
                        break;

                    case "3":
                        // End
                        Console.Beep(500, 100);
                        Console.Beep(300, 100);
                        Thread.Sleep(500);
                        validMainInput = true;
                        return true;

                    default:
                        // If input is not 1, 2, or 3
                        Console.Clear();
                        Console.WriteLine($"\'{mainInput}\' is not a valid selection dammit! Try again!");
                        Console.WriteLine();
                        Console.WriteLine("(1) Display Vending Machine Items");
                        Console.WriteLine("(2) Purchase");
                        Console.WriteLine("(3) End");
                        mainInput = Console.ReadLine();
                        break;
                }
            }

            return false;
        }

        public void Display()
        {
            Console.Clear();
            Console.WriteLine(String.Format("{0,-8}{1,-20}{2,8}{3,8}", "Slot", "Product","Price","Qty."));
            Console.WriteLine("--------------------------------------------");
            foreach (VendingMachineItem item in vendingMachine.GetItems())
            {
                if (item.NumberInStock > 0)
                {
                    Console.WriteLine(String.Format("{0,-8}{1,-20}{2,8}{3,6}", " " + item.ItemCode, item.Name, "$" + item.Price, item.NumberInStock));
                }
                else
                {
                    Console.WriteLine(String.Format("{0,-8}{1,-20}{2,10}{3,4}", " " + item.ItemCode, item.Name, "SOLD OUT", item.NumberInStock));
                }
            }
            Console.WriteLine();
        }

        public void TransactionMenu()
        {
            TransactionSession session = new TransactionSession();

            bool validTransInput = false;

            while (!validTransInput)
            {
                Console.WriteLine("(1) Feed Money");
                Console.WriteLine("(2) Select Product");
                Console.WriteLine("(3) Finish Transaction");
                Console.WriteLine($"Current Money Provided: ${session.Balance}");
                Console.WriteLine();
                Console.Write("Selection: ");
                string transInput = Console.ReadLine();

                switch (transInput)
                {
                    case "1":
                        // Feed Money
                        Console.Clear();
                        Console.Write("Please enter an accepted dollar amount (1, 2, 5, 10): ");
                        decimal billValue = 0.0M;
                        string inputFeed = Console.ReadLine();

                        while (!decimal.TryParse(inputFeed,out billValue))
                        {
                            Console.Clear();
                            Console.WriteLine($"\'{inputFeed}\' is not an accepted dollar amount! Try again!");
                            Console.Write("Please enter an accepted dollar amount (1, 2, 5, 10): ");
                            inputFeed = Console.ReadLine();
                        }

                        while (!session.Deposit(billValue))
                        {
                            Console.Clear();
                            Console.WriteLine($"\'{billValue}\' is not an accepted dollar amount! Try again!");
                            Console.Write("Please enter an accepted dollar amount (1, 2, 5, 10): ");
                            billValue = decimal.Parse(Console.ReadLine());
                        }

                        Console.Clear();
                        vendingMachine.WriteLineToLog("FEED MONEY:", billValue, session.Balance);

                        // 10/01/2018 12:00:00 PM FEED MONEY: $5.00 $5.00
                        // Date Time "FEED MONEY:" amount_added balance
                        break;

                    case "2":
                        // Select Product
                        Console.Clear();
                        Display();

                        Console.Write("Select an item code: ");
                        string input = Console.ReadLine();
                        string inputCode = input.ToUpper();
                        
                        if (!vendingMachine.IsValidCode(inputCode))
                        {
                            Console.Clear();
                            Console.WriteLine($"\'{input}\' is not a valid item code");
                            Console.WriteLine();

                        }
                        else
                        {
                            if (!vendingMachine.IsInStock(inputCode))
                            {
                                Console.Clear();
                                Console.WriteLine($"\'{inputCode}\' is out of stock");
                                Console.WriteLine();
                            }
                            else
                            {
                                // buy the item
                                if (vendingMachine.HaveEnoughToBuy(inputCode, session))
                                {
                                    decimal startingBalance = session.Balance;
                                    VendingMachineItem theItem = vendingMachine.BuyItem(inputCode, session);
                                    Console.Clear();
                                    Console.WriteLine($"User obtained one (1) {theItem.Name}");
                                    Console.WriteLine();
                                    Console.WriteLine(theItem.Text);
                                    Console.WriteLine();

                                    vendingMachine.WriteLineToLog($"{theItem.Name} {theItem.ItemCode}", startingBalance, session.Balance);
                                }
                                else
                                {
                                    Console.Clear();
                                    Console.WriteLine("Funds not sufficient ;(");
                                    Console.WriteLine();
                                }
                            }
                        }

                        // 10/01/2018 12:00:20 PM Crunchie B4 $10.00 $8.50
                        // Date Time itemName itemCode balanceBefore balanceAfter
                        break;

                    case "3":
                        // Finish Transaction
                        Console.Clear();
                        if (session.Balance > 0)
                        {
                            decimal changeBack = session.Balance;
                            string change = session.FinishTransaction();
                            Console.WriteLine("Change returned:");
                            Console.WriteLine();
                            Console.WriteLine(change);
                            Console.WriteLine();
                            vendingMachine.WriteLineToLog("GIVE CHANGE:", changeBack, session.Balance);
                        }
                        validTransInput = true;
                        // 10/01/2018 12:01:35 PM GIVE CHANGE: $7.50 $0.00
                        // Date Time "GIVE CHANGE:" balanceBefore balanceAfter
                        break;

                    default:
                        // If input is not 1, 2, or 3
                        Console.Clear();
                        Console.WriteLine($"\'{transInput}\' is not a valid selection dammit! Try again!");
                        Console.WriteLine();
                        Console.WriteLine("(1) Feed Money");
                        Console.WriteLine("(2) Select Product");
                        Console.WriteLine("(3) Finish Transaction");
                        Console.WriteLine($"Current Money Provided: ${session.Balance}");
                        transInput = Console.ReadLine();
                        break;
                }
            }
        }

       public void Welcome()
        {
            Console.Write(" *** ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("W");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("E");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("L");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("C");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write("O");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("M");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write("E");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write("!");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(" *** ");
        }

    }
}

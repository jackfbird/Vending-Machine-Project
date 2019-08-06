using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Capstone.Classes
{
    public class VendingMachine
    {
        private List<VendingMachineItem> items = new List<VendingMachineItem>();
        private string filePath = @"C:\VendingMachine\vendingmachine.csv";
        private string logPath = @"C:\VendingMachine\Log.txt";

        public void Initialize()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(logPath, false)) { }
            }
            catch
            {

            }

            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        VendingMachineItem newItem = null;

                        if (line.StartsWith('A'))
                        {
                            newItem = new Item_Chips(line);
                        }
                        else if (line.StartsWith('B'))
                        {
                            newItem = new Item_Candy(line);
                        }
                        else if (line.StartsWith('C'))
                        {
                            newItem = new Item_Beverages(line);
                        }
                        else if (line.StartsWith('D'))
                        {
                            newItem = new Item_Gum(line);
                        }

                        items.Add(newItem);
                    }
                }
            }
            catch (Exception e) // catch Exceptions
            {
                Console.WriteLine(e.Message);
            }
        }

        public List<VendingMachineItem> GetItems()
        {
            return items;
        }

        public void WriteLineToLog(string action, decimal money1, decimal money2)
        {
            string line = $"{DateTime.Now.ToString()} {action} ${money1.ToString("#.00")} ${money2}";

            try
            {
                using (StreamWriter writer = new StreamWriter(logPath, true))
                {
                    writer.WriteLine(line);
                    writer.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public bool IsValidCode(string code)
        {
            bool validCode = false;

            foreach(VendingMachineItem item in items)
            {
                if (item.ItemCode == code)
                {
                    validCode = true;
                }
            }

            return validCode;
        }

        public bool IsInStock(string code)
        {
            bool InStock = false;

            foreach(VendingMachineItem item in items)
            {
                if (item.ItemCode == code)
                {
                    if (item.NumberInStock > 0)
                    {
                        InStock = true;
                    }
                }
            }
            return InStock;
        }

        public bool HaveEnoughToBuy(string code, TransactionSession session)
        {
            bool hasEnough = false;

            foreach(VendingMachineItem item in items)
            {
                if (item.ItemCode == code)
                {
                    if (session.Balance >= item.Price)
                    {
                        hasEnough = true;
                    }
                }
            }

            return hasEnough;
        }

        public VendingMachineItem BuyItem(string code, TransactionSession session)
        {
            VendingMachineItem wantedItem = null;

            foreach (VendingMachineItem item in items)
            {
                if (item.ItemCode == code)
                {
                    decimal price = item.Price;
                    session.Deduct(price);
                    item.NumberInStock--;
                    wantedItem = item;
                }
            }

            return wantedItem;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class VendingMachineItem
    {
        // PROPERTIES
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int NumberInStock { get; set; }
        public string ItemCode { get; set; }
        public string Text { get; protected set; }

        protected VendingMachineItem(string line)
        {
            string[] splitLine = line.Split('|');
            ItemCode = splitLine[0];
            Name = splitLine[1];
            Price = decimal.Parse(splitLine[2]);
            NumberInStock = 5;
        }
    }
}

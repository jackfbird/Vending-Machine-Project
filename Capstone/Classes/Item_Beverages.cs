using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class Item_Beverages : VendingMachineItem
    {
        public Item_Beverages(string line) :base(line)
        {
            Text = "Glug Glug, Yum!";
        }
    }
}

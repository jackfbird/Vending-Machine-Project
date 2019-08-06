using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class Item_Chips : VendingMachineItem
    { 
        public Item_Chips(string line) : base(line)
        {
            Text = "Crunch Crunch, Yum!";
        }

    }
}

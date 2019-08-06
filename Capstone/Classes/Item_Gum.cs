using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class Item_Gum : VendingMachineItem
    {
        public Item_Gum(string line) : base(line)
        {
            Text = "Chew Chew, Yum!";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class Item_Candy : VendingMachineItem
    {
        public Item_Candy(string line) :base (line)
        {
            Text = "Munch Munch, Yum!";
        }
    }
}

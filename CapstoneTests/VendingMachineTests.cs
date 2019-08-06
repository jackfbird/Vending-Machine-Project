using System;
using System.Collections.Generic;
using System.Text;
using Capstone.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CapstoneTests
{
    [TestClass()]
    public class VendingMachineTests
    {
        VendingMachine vm = null;
        TransactionSession session = null;

        [TestInitialize]
        public void Initialize()
        {
            vm = new VendingMachine();
            session = new TransactionSession();
            session.Deposit(2.00M);
            vm.Initialize();
        }

        [TestMethod]
        public void GetItems()
        {
            Assert.IsNotNull(vm.GetItems());
        }

        [TestMethod]
        public void VendingItemTest()
        {
            Item_Chips chips = new Item_Chips("A2|Stackers|1.45");
            Assert.AreEqual("A2", chips.ItemCode);
            Assert.AreEqual("Stackers", chips.Name);
            Assert.AreEqual(1.45M, chips.Price);
            Assert.AreEqual(5, chips.NumberInStock);

            Item_Candy candy = new Item_Candy("B1|Moonpie|1.80");
            Assert.AreEqual("B1", candy.ItemCode);
            Assert.AreEqual("Moonpie", candy.Name);
            Assert.AreEqual(1.80M, candy.Price);
            Assert.AreEqual(5, candy.NumberInStock);

            Item_Beverages beverage = new Item_Beverages("C3|Mountain Melter|1.50");
            Assert.AreEqual("C3", beverage.ItemCode);
            Assert.AreEqual("Mountain Melter", beverage.Name);
            Assert.AreEqual(1.50M, beverage.Price);
            Assert.AreEqual(5, beverage.NumberInStock);

            Item_Gum gum = new Item_Gum("D4|Triplemint|0.75");
            Assert.AreEqual("D4", gum.ItemCode);
            Assert.AreEqual("Triplemint", gum.Name);
            Assert.AreEqual(0.75M, gum.Price);
            Assert.AreEqual(5, gum.NumberInStock);
        }

        [TestMethod]
        public void IsValidCode()
        {
            Assert.AreEqual(true, vm.IsValidCode("A1"));
            Assert.AreEqual(true, vm.IsValidCode("D3"));
            Assert.AreEqual(false, vm.IsValidCode("X9"));
        }

        [TestMethod]
        public void IsInStock()
        {
            Assert.AreEqual(true, vm.IsInStock("A1"));
            Assert.AreEqual(true, vm.IsInStock("B2"));
        }

        [TestMethod]
        public void HaveEnoughToBuy()
        {
            Assert.AreEqual(true, vm.HaveEnoughToBuy("D4", session));
            Assert.AreEqual(false, vm.HaveEnoughToBuy("A4", session));
        }

        [TestMethod]
        public void BuyItem()
        {
            VendingMachineItem newItem = vm.BuyItem("D4", session);
            Assert.IsNotNull(newItem);
            Assert.AreEqual("Triplemint", newItem.Name);
            Assert.AreEqual(4, newItem.NumberInStock);
        }




    }
}

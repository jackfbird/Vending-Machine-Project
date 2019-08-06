using System;
using System.Collections.Generic;
using System.Text;
using Capstone.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CapstoneTests
{
    [TestClass()]
    public class TransactionTests
    {
        [TestMethod]
        public void TransactionSession()
        {
            TransactionSession session = new TransactionSession();
            Assert.AreEqual(0.0M, session.Balance);
        }

        [TestMethod]
        public void Deposit()
        {
            TransactionSession session = new TransactionSession();
            Assert.AreEqual(false, session.Deposit(4.0M));
            Assert.AreEqual(true, session.Deposit(5.0M));
            Assert.AreEqual(5.0M, session.Balance);
        }

        [TestMethod]
        public void Deduct()
        {
            TransactionSession session = new TransactionSession();
            session.Deposit(5.0M);
            session.Deduct(1.0M);
            Assert.AreEqual(4.0M, session.Balance);
        }

        [TestMethod]
        public void FinishTransaction()
        {
            TransactionSession session = new TransactionSession();
            session.Deposit(5.0M);
            Assert.AreEqual("Quarters: 20\nDimes: 0\nNickles: 0\nTotal change: $5.00", session.FinishTransaction());
        }

    }
}

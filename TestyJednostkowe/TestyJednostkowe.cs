using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wspolbiezne;
using System;

namespace TestyJednostkowe
{
    [TestClass]
    public class TestyJednostkowe
    {
        private Kalkulator kalkulator = new(1);

        [TestMethod]
        public void TestDodawanie()
        {
            Assert.AreEqual(2, kalkulator.Add(1));
        }

        [TestMethod]
        public void TestOdejmowanie()
        {
            Assert.AreEqual(-32, kalkulator.Substract(33));
        }

        [TestMethod]
        public void TestMnozenie()
        {
            Assert.AreEqual(1, kalkulator.Mul(1));
        }

        [TestMethod]
        public void TestDzielenie()
        {
            Assert.AreEqual(0, kalkulator.Div(2));
            Assert.ThrowsException<ArithmeticException>(() => kalkulator.Div(0));

        }
    }
}
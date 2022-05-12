using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestyJednostkowe {
    [TestClass]
    public class PlainTest {

        [TestMethod]
        public void Test1() {
            LogicLayerAPI api = LogicLayerAPI.createAPI();
            Assert.ThrowsException<NullReferenceException>(
                () => api.GetSpheres());

            api.createPlain(10, 10);
            api.Make(10);

            Assert.AreEqual(10, api.GetSpheres().Count);
        }


        [TestMethod]
        public void Test2() {
            LogicLayerAPI api = LogicLayerAPI.createAPI();
            Assert.ThrowsException<NullReferenceException>(
                () => api.Make(10));

            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => api.createPlain(-10, 10));

            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => api.createPlain(10, -10));

            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => api.createPlain(0, 0));
        }

    }
}
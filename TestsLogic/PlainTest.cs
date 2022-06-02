using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestyJednostkowe {
    [TestClass]
    public class PlainTest {

        [TestMethod]
        public void Test1() {
            LogicLayerAPI api = LogicLayerAPI.createAPI();

            Assert.IsInstanceOfType(api, typeof(LogicLayerAPI));
        }


        [TestMethod]
        public void Test2() {
            LogicLayerAPI api = LogicLayerAPI.createAPI();
            api.Start(10);

            Assert.AreEqual(10, api.Spheres.Count);
        }

        [TestMethod]
        public void Test3() {
            LogicLayerAPI api = LogicLayerAPI.createAPI();
            api.Start(10);
            Assert.AreEqual(10, api.Spheres.Count);
            api.Stop();
            Assert.AreEqual(0, api.Spheres.Count);
        }

    }
}
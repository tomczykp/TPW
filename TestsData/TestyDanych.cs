using Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestyJednostkowe {
    [TestClass]
    public class TestyDanych {

        [TestMethod]
        public void Test1() {
            DataLayerAPI api = DataLayerAPI.createAPI();

            Assert.IsInstanceOfType(api, typeof(DataLayerAPI));
        }

        [TestMethod]
        public void Test2() {
            DataLayerAPI api = DataLayerAPI.createAPI();

            Assert.AreEqual(api.Width, 640);
            Assert.AreEqual(api.Height, 360);
        }

        [TestMethod]
        public void Test3() {
            DataLayerAPI api = DataLayerAPI.createAPI();
            api.Init(3);

            Assert.AreEqual(api.Spheres.Count, 3);
            foreach (SphereData s in api.Spheres)
                Assert.IsTrue((s.Weight <= 10 && s.Weight >= 1));
        }

    }
}
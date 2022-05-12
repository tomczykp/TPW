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



    }
}
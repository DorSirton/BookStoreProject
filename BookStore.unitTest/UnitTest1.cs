using BookStore.Server;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BookStore.unitTest
{
    [TestClass]
    public class UnitTest1
    {
        readonly ProdactService prodactservice = new ProdactService();

        [TestMethod]
        public void TestMethod1()
        {
            Assert.IsNotNull(prodactservice.GetAllProdact());
        }
        [TestMethod]
        public void TestMethod2()
        {
            Assert.IsNotNull(prodactservice.GetAllPurchaseProdact());
        }
        [TestMethod]
        public void TestMethod3()
        {
            Assert.IsNotNull(prodactservice.GetAllSaleProdact());
        }
    }
}

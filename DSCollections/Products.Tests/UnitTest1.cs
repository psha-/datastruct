using System;
using Wintellect.PowerCollections;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Products.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestAdd500KSearch10K()
        {
            var products = new OrderedMultiDictionary<float, string>(true);
            for( var i = 0; i<500000; i++)
            {
                products.Add(i/5000f, "Product "+i);
            }
            for (var i = 0; i < 10000; i++)
            {
                var productResults = products.Range(0, true, 1, true);
                Assert.AreEqual(productResults.Count, 5001);
            }
        }
    }
}

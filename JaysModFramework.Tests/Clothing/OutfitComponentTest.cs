﻿using JMF.Clothing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JMF.Tests
{
    [TestClass]
    public class OutfitComponentTest
    {
        [TestMethod]
        public void TestToJson()
        {
            //Assert.AreEqual("{\"Index\":0,\"Colors\":[\"Default\"],\"CurrentColor\":0,\"HasColor\":false}", new OutfitComponent().ToJSON());
        }
        [TestMethod]
        public void TestFromJson()
        {
            //Assert.AreEqual(
            //    new OutfitComponent(1, new string[] { "Foo" }, 0, true),
            //    new OutfitComponent("{\"Index\":1,\"Colors\":[\"Foo\"],\"CurrentColor\":0,\"HasColor\":true}")
            //    );
        }
    }
}

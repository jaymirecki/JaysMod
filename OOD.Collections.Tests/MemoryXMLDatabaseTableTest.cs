using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace OOD.Collections.Tests
{
    using Table = MemoryXMLDatabaseTable<string, TestItem>;
    public class TestItem : IXMLDatabaseItem<string>
    {
        public string ID { get; set; } = "ID string";
        public string Value { get; set; } = "Value string";
        public TestItem() { }
        public TestItem(string id, string value)
        {
            ID = id;
            Value = value;
        }
    }
    [TestClass]
    public class MemoryXMLDatabaseTableTest
    {
        private static string DIRECTORY { get; } = Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\TestValues");
        [TestMethod]
        public void TestConstructor()
        {
            Table table = new Table(DIRECTORY, "read");
        }
        [TestMethod]
        public void TestAdd()
        {
            Table table = new Table(DIRECTORY, "read");
            table.AddValue(new TestItem());
            Assert.IsTrue(table.TryGetValue(new TestItem().ID, out TestItem value));
        }
        [TestMethod]
        public void TestClearCache()
        {
            Table table = new Table(DIRECTORY, "read");
            table.AddValue(new TestItem());
            table.ClearCache();
            Assert.IsTrue(table.TryGetValue(new TestItem().ID, out TestItem value));
        }
    }
}

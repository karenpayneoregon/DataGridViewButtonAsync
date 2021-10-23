using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthEntityTestProject.Base;
using NorthWindCoreLibrary.Classes;

namespace NorthEntityTestProject
{
    [TestClass]
    public partial class MainTest : TestBase
    {
        [TestMethod]
        [TestTraits(Trait.EntityFrameworkCore)]
        public void ReadAllCustomersTest()
        {
            var results = Operations.LoadCustomerData();
            Assert.AreEqual(results.Count, 91);
        }
        [TestMethod]
        [TestTraits(Trait.PlaceHolder)]
        public void ReadContactTypesTest()
        {
            var results = Operations.LoadContactTypes();
            Assert.AreEqual(results.Count,12);
        }
    }
}

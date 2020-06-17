using CodeShellCore.Helpers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.UnitTest.Other
{
   public class StringFunctions : UnitTestClass
    {
        [SetUp]
        public void SetUp() { }

        [TestCase("1.0.0.0","1.0.0.1",2)]
        [TestCase("2.0.0.0", "1.3.0", 1)]
        [TestCase("2.0.0.0", "2.0.0.0", 0)]
        public void CompareVersions(string v1,string v2,int output)
        {
            var res = Utils.CompareVersions(v1, v2);
            Assert.AreEqual(output, res);
        }
    }
}

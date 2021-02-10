using CodeShellCore.Security.Authorization;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.UnitTest.Auth
{
    [TestFixture]
    public class PermissionTest : UnitTestClass
    {

        [Test]
        [TestCase(3, "11")]
        [TestCase(7, "111")]
        public void FromNumber(int num, string outPut)
        {
            Permission p = new Permission(num);
            Assert.AreEqual(outPut, p.ToString());
        }

        [Test]
        [TestCase(new[] { 2, 1, 4 }, "111")]
        [TestCase(new[] { 3, 1, 8 }, "1011")]
        public void Combine(int[] nums, string outPut)
        {
            Permission p = new Permission(0);
            foreach (var num in nums)
            {
                p.Append(num);
            }
            Assert.AreEqual(outPut, p.ToString());
        }

        [Test]
        [TestCase(3,2,true, "111")]
        [TestCase(7, 2, false, "11")]
        public void SetBit(int perm,int bitId,bool val,string outPut)
        {
            Permission p = new Permission(perm);
            p.SetBit(bitId, val);
            Assert.AreEqual(outPut, p.ToString());
        }

        [Test]
        [TestCase(3, 3, false)]
        [TestCase(9, 1, false)]
        [TestCase(9, 0, true)]
        public void GetBit(int perm, int bitId, bool val)
        {
            Permission p = new Permission(perm);
            var o = p.FromBit(bitId);
            Assert.AreEqual(val, o);
        }
    }
}

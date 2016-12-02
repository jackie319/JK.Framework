using System;
using JK.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {

           
            Eagle eagle=new Eagle();
            eagle.Name = "老鹰";
            eagle.Email = "jackie319@vip.qq.com";
            eagle.Phone = "18545454845";

             IBird sparrow =eagle;

            Assert.IsTrue(true);
        }
    }
}

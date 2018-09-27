using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JK.Framework.Extensions.Encryption;

namespace JK.Framework.Test
{
    [TestClass]
    public class DESTest
    {
        [TestMethod]
        public void TestDEs()
        {
            var jackie = DES.DESEncrypt("jackie214");
            var me = DES.DESDecrypt(jackie);
            Assert.IsTrue(me.Equals("jackie214"));
        }
    }
}

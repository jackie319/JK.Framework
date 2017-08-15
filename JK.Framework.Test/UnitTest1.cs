using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Text;
using JK.Framework.Extensions.QrCode;

namespace JK.Framework.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {

            IList<string> mobileList = new List<string>();
            mobileList.Add("18288215197");
            mobileList.Add("18288215197");
            mobileList.Add("18288215197");

            StringBuilder mobiles = new StringBuilder("[");
            int total = mobileList.Count;
            for (int i = 0; i < total; i++)
            {
                mobiles.Append("\"");
                mobiles.Append(mobileList[i]);
                mobiles.Append("\"");
                if (i != total - 1)
                {
                    mobiles.Append(",");
                }

            }
            mobiles.Append("]");
            var str = mobiles.ToString();
            Assert.IsNull(mobiles);
        }

        [TestMethod]
        public void TestQrCode()
        {
            TWQrCode.GenerateQrCode("http://m.maimaiyin.cn","D:\\erweima/","测试二维码22");
            Assert.IsTrue(true);
        }
    }
}

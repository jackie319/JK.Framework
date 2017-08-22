using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Text;
using JK.Framework.Extensions.QrCode;
using JK.Framework.Extensions.Zip;
using JK.Framework.Extensions.Encryption;
using JK.Framework.Extensions;
using System.Xml.Linq;

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
        public void TestJsonArray()
        {
            IList<string> mobileList = new List<string>();
            mobileList.Add("18288215197");
            mobileList.Add("18288215197");
            mobileList.Add("18288215197");
            System.Json.JsonArray array = new System.Json.JsonArray();
            foreach (var item in mobileList)
            {
                array.Add(item);
            }
            var str=array.ToString();
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void TestQrCode()
        {
            TWQrCode.GenerateQrCode("http://m.maimaiyin.cn","D:\\erweima/","测试二维码22");
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void TestZip()
        {
            ZipHelper.ZipFile("D:\\erweima/", "");
            Assert.IsTrue(true);
        }
        [TestMethod]
        public void TestDes()
        {
            string reqInfo = "yArzticOpSKuSEnHAw+qy78yYB846QSlwA7h92M24BzvegbMHObUPYIdBrUtIOLFG0iX3oKee0UQ+/Q9EV6UJ68EDbKpKBBR0SIxNnJtVQ9itUgbnscr9I3x6lTFED31BUBKo6Z8EQX9IxhapdJY8yumzFzv3QOub//vclrqNxP78Dt5fN1LKYjSbFaVv78/WhSVKnrVBx6O9YY7PKWCabMXT6cwf6Oj2vK78wEXJuHh88WVUB8K6R9XIC59Ay6KoIq/nqoY3Lc7fXPyrcD2ns2WiN0JPpuBlSJrchQo3YAjJg+dO6XDdD6naObHOWtXBeQ3ifjvmB1YHN/xRs4hCDCvTyQGSupL7th9wZciSHDP9+rbQMBZYGd5Q7nF3gkXT6XROaKxh8H1eKwSQAOrj9GWSxl0YXE3lHCQd4JcH3Tpe5Ms0w0bhE303fCH/mT2t6ODj2e4hNwU7uo9VYjxCCA3RHfzrZ91nuyoFwl1QIyNOGayKjk+rWF5wYlrfCwUn4QwbC6v6DDuQpbUmFQQYKp+zpCgItxFBsHk1rnNUW2l2RMS4wqH15WqruJfXR/mqOu7oLDgWVbikbp/dnPD7Mbcd5WnkZmkz9FCzLxEu0x+MdT0aVcODN/gY/aXejk0Uy8aZJ8XTkmxiagK/v3hiZ8UbXt/fHTi1DBB2xQIjnWcfBkyu0ml770aL84LDCsXhYxYvy/QrlcYNjCYFkm+RB71r2KmfBzajhmY1C+IfrkK+XnRB57aBL0TLkZ9Ygp0oi9saaV29tjqSAMzE2DdRrQe2Y4jTvVOMuzb/ZLHMgThQMDGv1+cgcU9Lix9FGmWghNgaJ2eglHoNBMDDml/4O1d8RuxJ+QJWEMVfGJVAvjIB086rYN02geiKwiKOvRr4+CGaj8d73eahTef9R31d0yTJrLLo+XQDrBHQj1lJKvro5BW8e910qp70oMCmtnDJDInc4ce51nHMFIyxDUJkJAHRScJBv11uwEUkNBcrAezdocpzHGPlpxBAxSLGLM2CB4/vxfMJmm/oLu++XgiWRRnAyG/RIdQQAJEOGWXGKo=";
           // string b=reqInfo.FromBase64Str();
            string keyStr = "9EFE9DE8918E47E0B1718D2F0ABFjklt";
            string key = keyStr.ToMd5();
            string result = AES.AESDecrypt(reqInfo,key);
            var res = XDocument.Parse(result);
            string TransactionId = GetXmlValue(res, "transaction_id");
            string OutTradeNo = GetXmlValue(res, "out_trade_no");
            string OutRefundNo = GetXmlValue(res, "out_refund_no");
            Assert.IsTrue(1==1);
        }

        private string GetXmlValue(XDocument res, string nodeName)
        {
            if (res == null || res.Element("root") == null
                || res.Element("root").Element(nodeName) == null)
            {
                return null;
            }
            return res.Element("root").Element(nodeName).Value;
        }

    }
}

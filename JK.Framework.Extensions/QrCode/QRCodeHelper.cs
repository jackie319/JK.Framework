using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThoughtWorks.QRCode.Codec;

namespace JK.Framework.Extensions.QrCode
{
    /// <summary>
    /// 生成带logo二维码
    /// </summary>
    public class QRCodeHelper
    {/// <summary>
     /// 创建二维码
     /// </summary>
     /// <param name="content"></param>
     /// <param name="size"></param>
     /// <returns></returns>
        public static Bitmap Create(string content)
        {
            try
            {
                QRCodeEncoder qRCodeEncoder = new QRCodeEncoder();
                qRCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;//设置二维码编码格式 
                qRCodeEncoder.QRCodeScale = 4;//设置编码测量度             
                qRCodeEncoder.QRCodeVersion = 7;//设置编码版本   
                qRCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;//设置错误校验 

                Bitmap image = qRCodeEncoder.Encode(content);
                return image;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 获取本地图片
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private static Bitmap GetLocalLog(string fileName)
        {
            Bitmap newBmp = new Bitmap(fileName);
            //Bitmap bmp = new Bitmap(newBmp);
            return newBmp;
        }
        /// <summary>
        /// 生成带logo二维码
        /// </summary>
        /// <returns></returns>
        public static Bitmap CreateQRCodeWithLogo(string content, string logopath,Boolean isNeedBorder=false)
        {
            //生成二维码
            Bitmap qrcode = Create(content);

            //生成logo
            Bitmap logo = GetLocalLog(logopath);
            Bitmap finalImage = ImageUtility.MergeQrImg(qrcode, logo, isNeedBorder);
            return finalImage;
        }

        /// <summary>
        /// 生成二维码（圆角带边框）
        /// </summary>
        /// <param name="content">二维码内容</param>
        /// <param name="logoPath">logo的本地绝对路径</param>
        /// <param name="savePath">保存的本地文件夹位置，不带“/”</param>
        /// <param name="picName">图片名称</param>
        /// <param name="isNeedBorder">是否需要加边框</param>
        public static void CreateQRCode(string content, string logoPath, string savePath,string picName,Boolean isNeedBorder = false)
        {
            string filename = picName + ".png";
            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }
            string filepath = savePath + "/" + filename;
            FileStream fs = new System.IO.FileStream(filepath, FileMode.OpenOrCreate, FileAccess.Write);
            var bitmap = QRCodeHelper.CreateQRCodeWithLogo(content, logoPath, isNeedBorder);
            bitmap.Save(fs, System.Drawing.Imaging.ImageFormat.Png);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace JK.Framework.Extensions
{

    /// <summary>
    /// ClassName:UpoadManager
    /// Version:1.0
    /// Date:2013/03/27
    /// Author:杨建宝(jackie)
    /// </summary>
    /// <remarks>
    /// 图片上传管理类
    /// </remarks>
    public static class UploadManager
    {

        //private const int ImgSize = 1024 * 1024;//图片大小限制
        private const int ImgSize = 4 * 1024 * 1024;//图片大小限制 //TODO:可配置
                                                    // private readonly static string[] ImgFormat = new string[] { "jpg", "jpeg", "png", "bmp", "gif" };//图片类型限制
        private readonly static FileExtension[] fileFormat = new FileExtension[] { FileExtension.JPG, FileExtension.BMP, FileExtension.GIF, FileExtension.PNG, };

        private static string DefuaultExtensionName = string.Empty;
        /// <summary>
        /// 上传的路径。此处存储路径并不在项目中，所以用绝对路径(项目部署虚拟引用)
        /// </summary>
        private static string uploadUrl = ConfigurationManager.AppSettings["UploadUrl"];
        /// <summary>
        /// 虚拟引用的名称:如：Upload 不用加“/”
        /// </summary>
        private static string uploadVirtualName = ConfigurationManager.AppSettings["UploadVirtualName"];
        /// <summary>
        /// 判断图片大小和格式
        /// </summary>
        /// <param name="img"></param>
        private static void CheckImg(HttpPostedFileBase img)
        {

            if (img.ContentLength > ImgSize)
            {
                throw new ArgumentException("图片大小超过限制！");
            }
            DefuaultExtensionName = IsAllowedExtension(img, fileFormat);

        }
        /// <summary>
        /// 根据相对路径返回保存在数据库中的相对路径(自动生成图片文件名)
        /// 例如：传入“/picture/kunming/2004”返回"Upload/picture/km/2004/12324343434.jpg" 
        /// </summary>
        /// <param name="img">图片流</param>
        /// <param name="uploadPath">相对路径（如：/picture/km/2004）</param>
        /// <returns>图片保存成功后返回的路径（即存储在数据库中的url）</returns>
        public static string SavePicture(HttpPostedFileBase img, string uploadPath)
        {
            string path = "";
            string picName = string.Empty;
            if (string.IsNullOrEmpty(img.FileName))
                throw new ArgumentException("图片为空！");
            if (string.IsNullOrEmpty(uploadPath))
                throw new ArgumentException("图片路径不能为空！");

            CheckImg(img);


            string basePath = uploadUrl;
            string typeName = DefuaultExtensionName;
            picName = Guid.NewGuid() + "." + typeName;
            path = uploadPath + "/" + picName;
            if (!Directory.Exists(basePath + uploadPath))
            {
                Directory.CreateDirectory(basePath + uploadPath);
            }
            img.SaveAs(basePath + path);


            // return uploadVirtualName + path;
            return picName;
        }

        /// <summary>
        /// 包含图片压缩
        /// </summary>
        /// <param name="img"></param>
        /// <param name="uploadPath"></param>
        /// <param name="needCompress"></param>
        /// <returns></returns>
        public static string SavePicture(HttpPostedFileBase img, string uploadPath, bool needCompress)
        {
            string resultPath = string.Empty;
            string compressPath = string.Empty;
            if (string.IsNullOrEmpty(img.FileName)) throw new ArgumentException("图片为空！");
            if (string.IsNullOrEmpty(uploadPath)) throw new ArgumentException("图片路径不能为空！");

            resultPath = SavePicture(img, uploadPath);


            if (!needCompress) return resultPath;

            //默认图片最大宽度和高度限制
            compressPath = ResizePic(400, 300, uploadPath, resultPath);


            return compressPath;
        }

        /// <summary>
        /// string img(base64)
        /// </summary>
        /// <param name="img">img为base64编码的图片字符串
        ///类似：“ data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAIQAAAARCAMAAAArMfRlAAAABGdBTUEA”
        /// </param>
        /// <param name="uploadPath"></param>
        /// <returns></returns>
        public static string SavePictureBase64(string img, string uploadPath)
        {
            //TODO：图片校验
            string path = "";
            string picName = string.Empty;
            if (string.IsNullOrEmpty(img))
                throw new ArgumentException("图片为空！");
            if (string.IsNullOrEmpty(uploadPath))
                throw new ArgumentException("图片路径不能为空！");
            var tmpArr = img.Split(',');
            byte[] bytes = Convert.FromBase64String(tmpArr[1]);
            MemoryStream ms = new MemoryStream(bytes);
            Bitmap bmp = new Bitmap(ms);
            string basePath = uploadUrl;
            picName =  Guid.NewGuid() + ".jpg";//TODO:
            path = uploadPath+"/" +picName;
            if (!Directory.Exists(basePath + uploadPath))
            {
                Directory.CreateDirectory(basePath + uploadPath);
            }
           if(bmp.Width>3000) throw new ArgumentException("图片太大！");//TODO:
            bmp.Save(basePath + path);
            ms.Close();
            return picName;
        }

        //controller 调用
        //public PictureViewModel UploadBase64()
        //{
        //    //获取base64编码的图片
        //    HttpContextBase context = (HttpContextBase)Request.Properties["MS_HttpContext"];
        //    string text = context.Request.Form["file"];
        //    PictureViewModel model = new PictureViewModel();
        //    var url = UploadManager.SavePictureBase64(text, "Member");
        //    model.PicUrl = url;
        //    return model;
        //}

        /// <summary>
        /// 具体参见MMY PictureController （Member）
        /// </summary>
        /// <param name="img"></param>
        /// <param name="uploadPath"></param>
        /// <returns></returns>
        public static string SavePictureForWebApi(string img, string uploadPath)
        {
            return "";
        }

        /// <summary>
        /// jpeg图片压缩
        /// </summary>
        /// <param name="sFile"></param>
        /// <param name="outPath"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        private static bool GetPicThumbnail(string sFile, string outPath, int flag)
        {
            System.Drawing.Image iSource = System.Drawing.Image.FromFile(sFile);
            ImageFormat tFormat = iSource.RawFormat;
            //以下代码为保存图片时，设置压缩质量
            EncoderParameters ep = new EncoderParameters();
            long[] qy = new long[1];
            qy[0] = flag;//设置压缩的比例1-100
            EncoderParameter eParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, qy);
            ep.Param[0] = eParam;
            try
            {
                ImageCodecInfo[] arrayICI = ImageCodecInfo.GetImageEncoders();
                ImageCodecInfo jpegICIinfo = null;
                for (int x = 0; x < arrayICI.Length; x++)
                {
                    if (arrayICI[x].FormatDescription.Equals("JPEG"))
                    {
                        jpegICIinfo = arrayICI[x];
                        break;
                    }
                }
                if (jpegICIinfo != null)
                {
                    iSource.Save(outPath, jpegICIinfo, ep);//dFile是压缩后的新路径
                }
                else
                {
                    iSource.Save(outPath, tFormat);
                }
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                iSource.Dispose();
                iSource.Dispose();
            }
        }


        /// <summary>
        /// 压缩图片,根据最大宽度等比压缩
        /// </summary>
        /// <returns></returns>
        private static string ResizePic(int maxWidth, int maxHeight, string uploadPath, string picPath)
        {
            string filePath = picPath.Replace(uploadVirtualName, uploadUrl);
            string fileName = picPath.Replace(uploadVirtualName, "").Replace(uploadPath, "");
            string path = string.Format("{0}{1}/Compress{2}", uploadUrl, uploadPath, fileName);
            string director = string.Format("{0}{1}/Compress", uploadUrl, uploadPath);
            string resultPath = string.Format("{0}{1}/Compress{2}", uploadVirtualName, uploadPath, fileName); ;
            if (!Directory.Exists(director))
            {
                Directory.CreateDirectory(director);
            }
            System.Drawing.Image imgPhoto =
            System.Drawing.Image.FromFile(filePath);
            int imgWidth = imgPhoto.Width;
            int imgHeight = imgPhoto.Height;
            bool flag = false;

            if (Array.IndexOf(imgPhoto.PropertyIdList, 274) > -1)
            {
                var orientation = (int)imgPhoto.GetPropertyItem(274).Value[0];
                switch (orientation)
                {
                    case 1:
                        break;
                    case 2:
                        imgPhoto.RotateFlip(RotateFlipType.RotateNoneFlipX);
                        break;
                    case 3:
                        imgPhoto.RotateFlip(RotateFlipType.Rotate180FlipNone);
                        break;
                    case 4:
                        imgPhoto.RotateFlip(RotateFlipType.Rotate180FlipX);
                        break;
                    case 5:
                        imgPhoto.RotateFlip(RotateFlipType.Rotate90FlipX);
                        flag = true;
                        break;
                    case 6:
                        imgPhoto.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        flag = true;
                        break;
                    case 7:
                        imgPhoto.RotateFlip(RotateFlipType.Rotate270FlipX);
                        flag = true;
                        break;
                    case 8:
                        imgPhoto.RotateFlip(RotateFlipType.Rotate270FlipNone);
                        flag = true;
                        break;
                }
                imgPhoto.RemovePropertyItem(274);
            }

            if (imgWidth > imgHeight) //如果宽度超过高度以宽度为准来压缩
            {
                if (imgWidth > maxWidth) //如果图片宽度超过限制
                {
                    float toImgWidth = maxWidth; //图片压缩后的宽度
                    float toImgHeight = imgHeight / (float)(imgWidth / toImgWidth); //图片压缩后的高度

                    if (flag)
                    {
                        System.Drawing.Bitmap img = new System.Drawing.Bitmap(imgPhoto, Convert.ToInt32(toImgHeight), Convert.ToInt32(toImgWidth));
                        img.Save(path);
                    }
                    else
                    {
                        System.Drawing.Bitmap img = new System.Drawing.Bitmap(imgPhoto, Convert.ToInt32(toImgWidth), Convert.ToInt32(toImgHeight));
                        img.Save(path);
                    }

                }
                else
                {
                    return picPath;
                }
            }
            else
            {
                if (imgHeight > maxHeight)
                {
                    float toImgHeight1 = maxHeight;
                    float toImgWidth1 = imgWidth / (float)(imgHeight / toImgHeight1);

                    if (flag)
                    {
                        System.Drawing.Bitmap img = new System.Drawing.Bitmap(imgPhoto, Convert.ToInt32(toImgHeight1), Convert.ToInt32(toImgWidth1));
                        img.Save(path);
                    }
                    else
                    {
                        System.Drawing.Bitmap img = new System.Drawing.Bitmap(imgPhoto, Convert.ToInt32(toImgWidth1), Convert.ToInt32(toImgHeight1));
                        img.Save(path);
                    }

                }
                else
                {
                    return picPath;
                }
            }
            return resultPath;
        }




        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="originalImagePath">源图路径（物理路径）</param>
        /// <param name="thumbnailPath">缩略图路径（物理路径）</param>
        /// <param name="towidth">缩略图指定宽度</param>
        /// <param name="toheight">缩略图指定高度</param>
        private static void MakeThumbnail(string originalImagePath, string thumbnailPath, double towidth, double toheight)
        {
            System.Drawing.Image originalImage = null;
            //新建一个bmp图片
            System.Drawing.Image bitmap = null;

            //新建一个画板
            System.Drawing.Graphics g = null;

            try
            {
                originalImage = System.Drawing.Image.FromFile(originalImagePath);

                double proportion1;
                double proportion2;
                int x = 0;
                int y = 0;
                int ow = originalImage.Width;//原图的宽
                int oh = originalImage.Height;//原图的高
                proportion1 = toheight / Convert.ToDouble(oh);
                proportion2 = towidth / Convert.ToDouble(ow);

                if (toheight > oh && towidth > ow) //如果宽高都小于要缩放的就不缩以与大小缩略
                {
                    toheight = oh;
                    towidth = ow;
                }
                else
                {
                    //根据比例设定相应的高宽
                    if (proportion1 > proportion2)
                    {
                        toheight = proportion2 * originalImage.Height;
                    }
                    else
                    {
                        towidth = proportion1 * originalImage.Width;
                    }
                }

                //新建一个bmp图片
                bitmap = new System.Drawing.Bitmap(Convert.ToInt32(towidth), Convert.ToInt32(toheight));

                //新建一个画板
                g = System.Drawing.Graphics.FromImage(bitmap);

                //设置高质量插值法
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

                //设置高质量,低速度呈现平滑程度
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                //清空画布并以透明背景色填充
                g.Clear(System.Drawing.Color.Transparent);

                //在指定位置并且按指定大小绘制原图片的指定部分
                g.DrawImage(originalImage, new System.Drawing.Rectangle(0, 0, Convert.ToInt32(towidth), Convert.ToInt32(toheight)),

                new System.Drawing.Rectangle(x, y, ow, oh),
                System.Drawing.GraphicsUnit.Pixel);
                //以jpg格式保存缩略图WebControls
                //File.Delete(thumbnailPath);
                bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch (Exception ex)
            {
                string a = ex.Message;
            }
            finally
            {
                try
                {
                    g.Dispose();
                    bitmap.Dispose();
                    originalImage.Dispose();
                }
                catch (Exception ex2)
                {
                    string a = ex2.Message;
                }
            }
        }


        public static string SavePicture(Stream fileStream, ImageFormat imageFormat, string uploadPath)
        {
            if (fileStream == null) throw new ArgumentNullException("fileStream");
            if (string.IsNullOrEmpty(uploadPath)) throw new ArgumentNullException("uploadPath");

            // 检查目录是否存在
            var directory = Path.Combine(uploadUrl, uploadPath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            // 配置扩展名
            var imgExt = string.Empty;
            if (imageFormat.Equals(ImageFormat.Png))
            {
                imgExt = "png";
            }

            if (imageFormat.Equals(ImageFormat.Jpeg))
            {
                imgExt = "jpg";
            }

            if (imageFormat.Equals(ImageFormat.Gif))
            {
                imgExt = "gif";
            }

            if (imageFormat.Equals(ImageFormat.Bmp))
            {
                imgExt = "bmp";
            }
            var filename = string.Format("{0}.{1}", Guid.NewGuid(), imgExt);
            var fileSavePath = Path.Combine(directory, filename);
            var file = File.Create(fileSavePath);
            var bm = new Bitmap(fileStream);
            bm.Save(file, imageFormat);
            file.Flush();
            file.Close();

            return Path.Combine(uploadVirtualName ?? string.Empty, uploadPath, filename);
        }


        private static string IsAllowedExtension(HttpPostedFileBase fu, FileExtension[] fileEx)
        {
            int fileLen = fu.ContentLength;
            byte[] imgArray = new byte[fileLen];
            fu.InputStream.Read(imgArray, 0, fileLen);
            MemoryStream ms = new MemoryStream(imgArray);
            System.IO.BinaryReader br = new System.IO.BinaryReader(ms);
            string fileclass = "";
            byte buffer;
            try
            {
                buffer = br.ReadByte();
                fileclass = buffer.ToString();
                buffer = br.ReadByte();
                fileclass += buffer.ToString();
            }
            catch
            {
            }
            br.Close();
            ms.Close();
            //注意将文件流指针还原
            fu.InputStream.Position = 0;
            foreach (FileExtension fe in fileEx)
            {
                if (Int32.Parse(fileclass) == (int)fe)
                    return fe.ToString().ToLower();

            }
            throw new ArgumentException("图片格式不符合要求格式（jpg,png,bmp,gif）！");
        }
    }

    public enum FileExtension
    {
        GIF = 7173,
        JPG = 255216,
        BMP = 6677,
        PNG = 13780,
        DOC = 208207,
        DOCX = 8075,
        XLSX = 8075,
        JS = 239187,
        XLS = 208207,
        SWF = 6787,
        MID = 7784,
        RAR = 8297,
        ZIP = 8075,
        XML = 6063,
        TXT = 7067,
        MP3 = 7368,
        WMA = 4838,

        // 239187 aspx
        // 117115 cs
        // 119105 js
        // 210187 txt
        //255254 sql 		
        // 7790 exe dll,
        // 8297 rar
        // 6063 xml
        // 6033 html
    }
}

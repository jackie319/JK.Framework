using JK.Data.Model;
using JK.PictureCenter.WebApi.Models;
using JK.Pictures;
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Http;

namespace JK.PictureCenter.WebApi.Controllers
{
    [RoutePrefix("Picture")]
    public class PictureController : ApiController
    {
        private ILog _log;
        private IPicture _Picture;
        public PictureController(IPicture picture)
        {
            _Picture = picture;
            _log = LogManager.GetLogger(typeof(PictureController));
        }
        /// <summary>
        /// 图片上传
        /// </summary>
        /// <returns></returns>
        [Route("Upload")]
        [HttpPost]
        public async Task<PictureViewModel> PostFormData()
        {
            // Check if the request contains multipart/form-data.
            //https://docs.microsoft.com/zh-cn/aspnet/web-api/overview/advanced/sending-html-form-data-part-2
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            string uploadUrl = WebConfigurationManager.AppSettings["UploadUrl"];
            string pictureUrl = WebConfigurationManager.AppSettings["PictureUrl"];

            string uploadCache = uploadUrl + "UploadCache";
            PictureViewModel model = new PictureViewModel();
            //保存到临时目录
            var provider = new MultipartFormDataStreamProvider(uploadCache);

            // Read the form data.
            await Request.Content.ReadAsMultipartAsync(provider);

            // This illustrates how to get the file names.
            //foreach (MultipartFileData file in provider.FileData)
            //{
            //        多张图片处理
            //}

            _log.Info("多少个：" + provider.FileData.Count);
            if (provider.FileData.Count < 1)
            {
                throw new ArgumentException("请上传文件");
            }
            //单张处理
            var file = provider.FileData[0];
            FileInfo fileinfo = new FileInfo(file.LocalFileName);
            if (fileinfo.Length < 0)
            {
                throw new ArgumentException("请上传文件");
            }
            if (fileinfo.Length > 4 * 1024 * 1024)
            {
                throw new ArgumentException("图片大小超过限制！");
            }

            string newfileName = Guid.NewGuid().ToString("") + ".jpg"; ;
            fileinfo.CopyTo(Path.Combine(uploadUrl, newfileName), true);
            fileinfo.Delete();
            //保存到数据库
            var entity = new Picture();
            entity.PictureUrl = newfileName;
            var result = _Picture.CreatedAdPic(entity);
            //返回
            model.Guid = result.Guid;
            model.HttpUrl = pictureUrl + result.Guid;
            return model;
        }

        [Route("")]
        [HttpGet]
        public HttpResponseMessage Detail(Guid pictureGuid)
        {
            var picture = _Picture.Find(pictureGuid);
            string uploadUrl = WebConfigurationManager.AppSettings["UploadUrl"];
            var imgPath = uploadUrl + picture.PictureUrl;
            //从图片中读取byte
            var imgByte = File.ReadAllBytes(imgPath);
            //从图片中读取流
            var imgStream = new MemoryStream(File.ReadAllBytes(imgPath));
            var resp = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(imgStream)
                //或者
                //Content = new ByteArrayContent(imgByte)
            };
            resp.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpg");
            return resp;
        }
    }
}

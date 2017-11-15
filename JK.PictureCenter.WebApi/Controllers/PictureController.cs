using JK.PictureCenter.WebApi.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace JK.PictureCenter.WebApi.Controllers
{
    public class PictureController : ApiController
    {
        private ILog _log = LogManager.GetLogger(typeof(PictureController));

        /// <summary>
        /// 图片上传
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [HttpPost]
        public async Task<PictureViewModel> PostFormData()
        {
            // Check if the request contains multipart/form-data.
            //https://docs.microsoft.com/zh-cn/aspnet/web-api/overview/advanced/sending-html-form-data-part-2
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            string uploadUrl = AppSetting.Instance().UploadUrl + "ProductLibrary";

            string uploadCache = AppSetting.Instance().UploadUrl + "UploadCache";
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
            model.PicUrl = newfileName;
            model.HttpUrl = AppSetting.Instance().ProductLibraryPictureUrl + newfileName;

            return model;
        }
    }
}

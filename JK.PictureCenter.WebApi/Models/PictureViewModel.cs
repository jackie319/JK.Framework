using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JK.PictureCenter.WebApi.Models
{
    public class PictureViewModel
    {
        /// <summary>
        /// 图片的Guid(提交给接口保存)
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// 用于显示、预览
        /// </summary>
        public string HttpUrl { get; set; }
    }
}
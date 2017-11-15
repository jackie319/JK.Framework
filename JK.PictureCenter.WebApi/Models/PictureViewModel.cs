using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JK.PictureCenter.WebApi.Models
{
    public class PictureViewModel
    {
        /// <summary>
        /// 提交的值
        /// </summary>
        public string PicUrl { get; set; }
        /// <summary>
        /// 用于显示
        /// </summary>
        public string HttpUrl { get; set; }
    }
}
using JK.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JK.CommonApi.WebApi.Models.FriendlyLinkModel
{
    public class FriendyLinkListViewModel
    {
        public Guid Guid { get; set; }
        /// <summary>
        /// 友情链接名称
        /// </summary>
        public string WebTitle { get; set; }
        /// <summary>
        /// 跳转地址
        /// </summary>
        public string WebUrl { get; set; }
        /// <summary>
        /// 显示顺序
        /// </summary>
        public int DisplayOrder { get; set; }
        /// <summary>
        /// 是否在前端显示
        /// </summary>
        public bool IsDisplay { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string TimeCreated { get; set; }

        public static FriendyLinkListViewModel CopyFrom(FriendlyLink link)
        {
            FriendyLinkListViewModel model = new FriendyLinkListViewModel();
            model.Guid = link.Guid;
            model.WebTitle = link.WebTitle;
            model.WebUrl = link.WebUrl;
            model.DisplayOrder = link.DisplayOrder;
            model.IsDisplay = link.IsDisplay;
            model.TimeCreated = link.TimeCreated.ToString("yyyy-MM-dd HH:mm:ss");
            return model;
        }
    }
}
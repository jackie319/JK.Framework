using JK.Data.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JK.CommonApi.WebApi.Models.FriendlyLinkModel
{
    public class FriendyLinkViewModel
    {
        public System.Guid Guid { get; set; }
        /// <summary>
        /// 友情链接名称
        /// </summary>
        [Required]
        public string WebTitle { get; set; }
        /// <summary>
        /// 跳转地址
        /// </summary>
        [Required]
        public string WebUrl { get; set; }
        /// <summary>
        /// 显示顺序
        /// </summary>
        public int DisplayOrder { get; set; }

        public static FriendyLinkViewModel CopyFrom(FriendlyLink link)
        {
            FriendyLinkViewModel model = new FriendyLinkViewModel();
            model.Guid = link.Guid;
            model.WebTitle = link.WebTitle;
            model.WebUrl = link.WebUrl;
            model.DisplayOrder = link.DisplayOrder;
            return model;
        }
        public FriendlyLink CopyTo()
        {
            FriendlyLink entity = new FriendlyLink();
            entity.Guid = Guid;
            entity.WebTitle = WebTitle;
            entity.WebUrl = WebUrl;
            entity.DisplayOrder = DisplayOrder;
            return entity;
        }
    }
}
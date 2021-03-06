﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JK.Frameword.Oauth.Wechat
{
    public class UserInfoResult
    {
        public string openid { get; set; }

        public string nickname { get; set; }

        /// <summary>
        /// 用户的性别，值为1时是男性，值为2时是女性，值为0时是未知
        /// </summary>
        public string sex { get; set; }

        public string province { get; set; }

        public string city { get; set; }

        public string country { get; set; }
        public string headimgurl { get; set; }

        /// <summary>
        /// 用户特权信息，json 数组，如微信沃卡用户为（chinaunicom）
        /// </summary>
        public IList<Object> privilege { get; set; }
        /// <summary>
        /// 只有在用户将公众号绑定到微信开放平台帐号后，才会出现该字段
        /// </summary>
        public string unionid { get; set; }
      
        /// <summary>
        /// errcode 不为空则代表出错
        /// </summary>
        public string errcode { get; set; }

        public string errmsg { get; set; }
    }
}

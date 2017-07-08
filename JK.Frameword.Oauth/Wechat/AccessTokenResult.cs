using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JK.Frameword.Oauth.Wechat
{
    public class AccessTokenResult
    {
        public string access_token { get; set; }

        public int expires_in { get; set; }

        public string refresh_token { get; set; }

        public string openid { get; set; }

        public string scope { get; set; }

        public string errcode { get; set; }

        public string errmsg { get; set; }
    }


    public class CheckAccessTokenResult
    {

        public string errcode { get; set; }

        public string errmsg { get; set; }
    }
}

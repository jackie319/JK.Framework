using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JK.JKUserAccount.ServiceModel
{
    public class WxJscodeLoginModel
    {
        [Required]
        public string Code { get; set; }

        /// <summary>
        /// 推荐人（分享人）
        /// </summary>
        public Guid UserGuid { get; set; }

        [Required]
        public string NickName { get; set; }

        [Required]
        public string AvatarUrl { get; set; }

        [Required]
        public string Gender { get; set; }

        public string City { get; set; }
        public string Province { get; set; }

        public string country { get; set; }

        public string Language { get; set; }
    }
}

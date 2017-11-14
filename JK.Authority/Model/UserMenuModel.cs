using JK.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JK.Authority.Model
{
    public class UserMenuModel
    {
        public Guid FunctionGuid { set; get; }

        public Guid ParentGuid { set; get; }

        public string FunctionName { set; get; }

        public string DisplayName { set; get; }

        public string ActionUrl { set; get; }

        /// <summary>
        /// 一级菜单，二级菜单，具体功能
        /// </summary>
        public string FunctionType { set; get; }

        public IList<UserMenuModel> ChildrenModels { set; get; }

        public static UserMenuModel CopyFrom(AuthorityFunction record)
        {
            if (record == null) return null;

            return new UserMenuModel
            {
                ActionUrl = record.ActionUrl,
                DisplayName = record.DisplayName,
                FunctionGuid = record.FunctionGuid,
                FunctionName = record.FunctionName,
                FunctionType = record.FunctionType,
                ParentGuid = record.ParentGuid,
                ChildrenModels = new List<UserMenuModel>(),
            };
        }
    }
}

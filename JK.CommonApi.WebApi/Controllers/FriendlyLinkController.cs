using JK.CommonApi.WebApi.App_Start;
using JK.CommonApi.WebApi.Models.FriendlyLinkModel;
using JK.Framework.API.Controller;
using JK.Framework.API.Filter;
using JK.Framework.API.Model;
using JK.JKUserAccount.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace JK.CommonApi.WebApi.Controllers
{
    [RoutePrefix("FriendlyLink")]
    [ApiSessionAuthorize]
    public class FriendlyLinkController : ApiController
    {
        private IFriendlyLink _FriendyLink;
        public FriendlyLinkController(IFriendlyLink friendyLink)
        {
            _FriendyLink = friendyLink;
        }

        /// <summary>
        /// 所有友情链接(后台)
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [Route("List")]
        [HttpGet]
        public IList<FriendyLinkListViewModel> GetList([FromUri]FriendyLinkQueryModel query)
        {
            if (query == null) query = new FriendyLinkQueryModel();
            int total;
            var list = _FriendyLink.GetList(query.Title, query.Display, 0, int.MaxValue, out total);
            var resultList = list.Select(FriendyLinkListViewModel.CopyFrom).ToList();
            BaseApiController.AppendHeaderTotal(total);
            return resultList;
        }

        /// <summary>
        /// 所有友情链接(前端显示)
        /// </summary>
        /// <returns></returns>
        [Route("Lists")]
        [HttpGet]
        public IList<FriendyLinkListViewModel> GetList()
        {
            int total;
            var list = _FriendyLink.GetList(string.Empty, true, 0, int.MaxValue, out total);
            var resultList = list.Select(FriendyLinkListViewModel.CopyFrom).ToList();
            BaseApiController.AppendHeaderTotal(total);
            return resultList;
        }


        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        [Route("{guid}")]
        [HttpGet]
        public FriendyLinkViewModel Detail(Guid guid)
        {
            var entity = _FriendyLink.Find(guid);
            var result = FriendyLinkViewModel.CopyFrom(entity);
            return result;
        }

        /// <summary>
        /// 新建或编辑友情链接
        /// </summary>
        /// <param name="model"></param>
        [Route("Save")]
        [HttpPost]
        [ApiValidationFilter]
        public ApiResultModel Save([FromBody] FriendyLinkViewModel model)
        {
            var entity = model.CopyTo();
            if (model.Guid == Guid.Empty)
            {
                _FriendyLink.CreateLink(entity);
            }
            else
            {
                _FriendyLink.UpdateLink(entity);
            }
            return this.ResultApiSuccess();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        [Route("{guid}")]
        [HttpDelete]
        public ApiResultModel Delete(Guid guid)
        {
            _FriendyLink.DeleteLink(guid);
            return this.ResultApiSuccess();
        }

        /// <summary>
        /// 显示
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        [Route("Display")]
        [HttpPost]
        public ApiResultModel Dispaly(Guid guid)
        {
            _FriendyLink.Display(guid, true);
            return this.ResultApiSuccess();
        }

        /// <summary>
        /// 不显示
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        [Route("NoDisplay")]
        [HttpPost]
        public ApiResultModel NoDispaly(Guid guid)
        {
            _FriendyLink.Display(guid, false);
            return this.ResultApiSuccess();
        }
    }
}

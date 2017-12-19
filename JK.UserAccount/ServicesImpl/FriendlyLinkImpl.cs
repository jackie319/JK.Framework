using JK.Data.Model;
using JK.Framework.Core.Data;
using JK.JKUserAccount.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JK.JKUserAccount.ServicesImpl
{
    public class FriendlyLinkImpl : IFriendlyLink
    {
        private IRepository<FriendlyLink> _FriendyLinkRepository;

        public FriendlyLinkImpl(IRepository<FriendlyLink> friendyLinkRepository)
        {
            _FriendyLinkRepository = friendyLinkRepository;
        }

        /// <summary>
        /// 友情链接类别
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public IList<FriendlyLink> GetList(string title, Boolean? display, int skip, int take, out int total)
        {
            var query = _FriendyLinkRepository.Table.Where(q => !q.IsDeleted);
            if (!string.IsNullOrEmpty(title))
            {
                query = query.Where(q => q.WebTitle.Contains(title));
            }
            if (display != null)
            {
                query = query.Where(q => q.IsDisplay == display);
            }
            total = query.Count();
            return query.OrderBy(q => q.DisplayOrder).ThenByDescending(q => q.TimeCreated).Skip(skip).Take(take).ToList();
        }

        public FriendlyLink Find(Guid linkGuid)
        {
            var entity = _FriendyLinkRepository.Table.FirstOrDefault(q => q.Guid == linkGuid && !q.IsDeleted);
            if (entity == null) throw new ArgumentException("找不到该实体");
            return entity;
        }

        public void CreateLink(FriendlyLink link)
        {
            link.Guid = Guid.NewGuid();
            link.TimeCreated = DateTime.Now;
            link.IsDeleted = false;
            link.IsDisplay = true;
            _FriendyLinkRepository.Insert(link);
        }

        public void UpdateLink(FriendlyLink link)
        {
            var entity = _FriendyLinkRepository.Table.FirstOrDefault(q => q.Guid == link.Guid && !q.IsDeleted);
            if (entity == null) throw new ArgumentException("找不到该实体");
            entity.WebTitle = link.WebTitle;
            entity.WebUrl = link.WebUrl;
            entity.DisplayOrder = link.DisplayOrder;
            _FriendyLinkRepository.Update(entity);
        }

        public void Display(Guid linkGuid, Boolean display)
        {
            var entity = _FriendyLinkRepository.Table.FirstOrDefault(q => q.Guid == linkGuid && !q.IsDeleted);
            if (entity == null) throw new ArgumentException("找不到该实体");
            entity.IsDisplay = display;
            _FriendyLinkRepository.Update(entity);
        }

        public void DeleteLink(Guid linkGuid)
        {
            var entity = _FriendyLinkRepository.Table.FirstOrDefault(q => q.Guid == linkGuid && !q.IsDeleted);
            if (entity == null) throw new ArgumentException("找不到该实体");
            entity.IsDeleted = true;
            _FriendyLinkRepository.Update(entity);
        }
    }
}

using JK.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JK.JKUserAccount.IServices
{
    public interface IFriendlyLink
    {
        IList<FriendlyLink> GetList(string title, Boolean? display, int skip, int take, out int total);
        FriendlyLink Find(Guid linkGuid);
        void CreateLink(FriendlyLink link);
        void UpdateLink(FriendlyLink link);
        void Display(Guid linkGuid, Boolean display);
        void DeleteLink(Guid linkGuid);

    }
}

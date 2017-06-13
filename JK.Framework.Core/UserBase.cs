using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace JK.Framework.Core
{
    public class UserBase :IPrincipal{
        public bool IsInRole(string role)
        {
            return true;
        }

        public UserBase(string name, string authenticationType, bool isAuthenticated)
        {
            Identity=new IdentityBase(name,authenticationType,isAuthenticated);
        }

        public IIdentity Identity { get; }
    }


    public class IdentityBase : IIdentity
    {
        public IdentityBase(string name, string authenticationType, bool isAuthenticated)
        {
            Name = name;
            AuthenticationType = authenticationType;
            IsAuthenticated = isAuthenticated;
        }
        public string Name { get; }
        public string AuthenticationType { get; }
        public bool IsAuthenticated { get; }
    }
   
}

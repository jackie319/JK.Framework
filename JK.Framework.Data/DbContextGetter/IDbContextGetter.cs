using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JK.Framework.Data
{
    public interface IDbContextGetter
    {
        T GetByName<T>(string name);
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JK.Test
{
    public interface IBird
    {
        string Name { set; get; }

        void Fly();
    }
}

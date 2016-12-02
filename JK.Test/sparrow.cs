using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JK.Test
{
   public class Sparrow:IBird
    {
       public string Name { get; set; }
       public void Fly()
       {
           throw new NotImplementedException();
       }
    }
}

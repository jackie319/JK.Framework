using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JK.Test
{
   public  class Eagle:IBird
    {
       public string Name { get; set; }

        public string Email { set; get; }
        public string Phone { set; get; }

       public void Run()
       {
           
       }
       public void Fly()
       {
           throw new NotImplementedException();
       }
    }
}

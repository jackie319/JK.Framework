using JK.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JK.Pictures
{
    public interface IPicture
    {
        void CreatedAdPic(Picture picture);
        Picture Find(Guid pictureGuid);
    }
}

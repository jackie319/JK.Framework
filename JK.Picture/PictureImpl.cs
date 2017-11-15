using JK.Data.Model;
using JK.Framework.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JK.Pictures
{
   public class PictureImpl:IPicture
    {
        private IRepository<Picture> _PictureRepository;
        public PictureImpl(IRepository<Picture> pictureRepository)
        {
            _PictureRepository = pictureRepository;
        }

        public Picture CreatedAdPic(Picture picture)
        {
            picture.TimeCreated = DateTime.Now;
            picture.Guid = Guid.NewGuid();
            picture.Type = string.Empty;
            picture.IsDeleted = false;
            picture.DisplayOrder = 0;
            _PictureRepository.Insert(picture);
            return picture;
        }

        public Picture Find(Guid pictureGuid)
        {
           var entity= _PictureRepository.Table.FirstOrDefault(q=>q.Guid==pictureGuid);
            if (entity == null) throw new ArgumentException("找不到该记录");
            return entity;
        }
    }
}

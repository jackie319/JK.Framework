//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace JK.Data.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Store
    {
        public System.Guid Guid { get; set; }
        public int Id { get; set; }
        public System.Guid StorePictureGuid { get; set; }
        public string StoreName { get; set; }
        public string Addreess { get; set; }
        public string Detail { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public System.Data.Entity.Spatial.DbGeography GeographicalPosition { get; set; }
        public System.DateTime TimeCreated { get; set; }
        public bool IsDeleted { get; set; }
    }
}
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
    
    public partial class ProductParameters
    {
        public System.Guid Guid { get; set; }
        public int Id { get; set; }
        public System.Guid ProductGuid { get; set; }
        public string Brand { get; set; }
        public string TimeToMarket { get; set; }
        public string Material { get; set; }
        public string MosaicMaterial { get; set; }
        public string PlaceOfOrigin { get; set; }
        public string Style { get; set; }
        public string ApplyPeople { get; set; }
        public string SalesChannels { get; set; }
        public System.DateTime TimeCreated { get; set; }
    
        public virtual Product Product { get; set; }
    }
}

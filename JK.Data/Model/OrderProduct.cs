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
    
    public partial class OrderProduct
    {
        public System.Guid Guid { get; set; }
        public System.Guid OrderGuid { get; set; }
        public System.Guid ProductGuid { get; set; }
        public string ProductName { get; set; }
        public string DefaultPic { get; set; }
        public System.Guid ClassificationGuid { get; set; }
        public string ClassificationName { get; set; }
        public int ProductPrice { get; set; }
        public int PromotionPrice { get; set; }
        public int ProductNumber { get; set; }
        public System.DateTime TimeCreated { get; set; }
    
        public virtual Order Order { get; set; }
    }
}

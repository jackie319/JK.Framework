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
    
    public partial class ProductPurchaseRecords
    {
        public System.Guid Guid { get; set; }
        public int Id { get; set; }
        public System.Guid ProductGuid { get; set; }
        public string ProductName { get; set; }
        public System.Guid ClassificationGuid { get; set; }
        public string ClassificationName { get; set; }
        public int Grams { get; set; }
        public int BuyingPrice { get; set; }
        public System.Guid SupplierGuid { get; set; }
        public string SupplierName { get; set; }
        public string OperatorName { get; set; }
        public System.Guid OperatorGuid { get; set; }
        public string Purchaser { get; set; }
        public int Number { get; set; }
        public bool IsDeleted { get; set; }
        public string Remark { get; set; }
        public System.DateTime TimeCreated { get; set; }
    
        public virtual Product Product { get; set; }
    }
}

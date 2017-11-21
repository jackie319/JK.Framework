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
    
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            this.ProductAlbum = new HashSet<ProductAlbum>();
            this.ProductClassification = new HashSet<ProductClassification>();
            this.ProductParameters = new HashSet<ProductParameters>();
            this.ProductPurchaseRecords = new HashSet<ProductPurchaseRecords>();
        }
    
        public System.Guid Guid { get; set; }
        public int Id { get; set; }
        public string ProductName { get; set; }
        public System.Guid CategoryGuid { get; set; }
        public int ProductNumber { get; set; }
        public string ProductNo { get; set; }
        public string DefaultPic { get; set; }
        public string SaleTitle { get; set; }
        public string SaleSubTitle { get; set; }
        public int Price { get; set; }
        public int PromotionPrice { get; set; }
        public string ProductDetail { get; set; }
        public string ProductRemark { get; set; }
        public string Status { get; set; }
        public bool IsSpecialOffer { get; set; }
        public bool IsRecommended { get; set; }
        public bool IsAd { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsDeleted { get; set; }
        public int ImaginaryNumber { get; set; }
        public int VisitedTotal { get; set; }
        public int SoldTotal { get; set; }
        public int EvaluationToTal { get; set; }
        public System.DateTime TimeOnShelf { get; set; }
        public System.DateTime TimeOffShelf { get; set; }
        public System.DateTime TimeCreated { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductAlbum> ProductAlbum { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductClassification> ProductClassification { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductParameters> ProductParameters { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductPurchaseRecords> ProductPurchaseRecords { get; set; }
    }
}

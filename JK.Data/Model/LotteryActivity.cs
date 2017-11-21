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
    
    public partial class LotteryActivity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LotteryActivity()
        {
            this.LotteryHistory = new HashSet<LotteryHistory>();
            this.LotteryPrize = new HashSet<LotteryPrize>();
        }
    
        public System.Guid Guid { get; set; }
        public int Id { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public int BaseNumber { get; set; }
        public string DefaultPic { get; set; }
        public string Detail { get; set; }
        public System.DateTime BeginTime { get; set; }
        public System.DateTime EndTime { get; set; }
        public bool IsDeleted { get; set; }
        public System.DateTime TimeCreated { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LotteryHistory> LotteryHistory { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LotteryPrize> LotteryPrize { get; set; }
    }
}

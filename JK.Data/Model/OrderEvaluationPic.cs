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
    
    public partial class OrderEvaluationPic
    {
        public System.Guid Guid { get; set; }
        public int Id { get; set; }
        public System.Guid EvaluationGuid { get; set; }
        public System.Guid ImageGuid { get; set; }
        public bool IsDeleted { get; set; }
        public System.DateTime TimeCreated { get; set; }
    
        public virtual OrderEvaluation OrderEvaluation { get; set; }
    }
}

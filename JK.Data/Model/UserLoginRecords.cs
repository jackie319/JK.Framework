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
    
    public partial class UserLoginRecords
    {
        public System.Guid Guid { get; set; }
        public int Id { get; set; }
        public System.Guid UserGuid { get; set; }
        public string Channel { get; set; }
        public int SessionTotal { get; set; }
        public string IP { get; set; }
        public string UserAgent { get; set; }
        public System.DateTime TimeCreated { get; set; }
    }
}

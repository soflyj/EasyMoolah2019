//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EasyMoolah.Repository
{
    using System;
    using System.Collections.Generic;
    
    public partial class Nedbank
    {
        public int Key { get; set; }
        public Nullable<int> ApplicationKey { get; set; }
        public string LightToken { get; set; }
        public string HeavyToken { get; set; }
        public string IntentId { get; set; }
        public string NedbankAuthorisationURL { get; set; }
        public string Code { get; set; }
        public string OfferId { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> ChangedDate { get; set; }
    
        public virtual Application Application { get; set; }
    }
}

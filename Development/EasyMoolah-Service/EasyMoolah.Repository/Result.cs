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
    
    public partial class Result
    {
        public int Key { get; set; }
        public Nullable<int> ApplicationKey { get; set; }
        public string Company_Name { get; set; }
        public string Company_Logo_Path { get; set; }
        public string Company_Logo_URL { get; set; }
        public string Website { get; set; }
        public Nullable<int> Rank { get; set; }
        public Nullable<decimal> Probability { get; set; }
        public Nullable<System.DateTime> RequestDateTime { get; set; }
        public string FincheckLeadReference { get; set; }
        public Nullable<bool> Callback { get; set; }
    }
}

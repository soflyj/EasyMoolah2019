﻿using System;
using System.Collections.Generic;

namespace EasyMoolah.Repository.Models
{
    public partial class Lead
    {
        public int Key { get; set; }
        public int? ApiId { get; set; }
        public string Id { get; set; }
        public string Status { get; set; }
        public string Code { get; set; }
    }
}
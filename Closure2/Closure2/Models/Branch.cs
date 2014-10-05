﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Closure2.Models
{
    public class Branch
    {
        public int ID { set; get; }
        public string Name { set; get; }
        public string Street { set; get; }
        public float latitude { set; get; }
        public float longitude { set; get; }
        public virtual ICollection<ProductModels> Products { get; set; }
    }
}
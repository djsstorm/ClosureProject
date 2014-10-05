using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Closure2.Models
{
    public class ProductModels
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }

        //public virtual ICollection<PostModels> Posts { get; set; }
    }

}
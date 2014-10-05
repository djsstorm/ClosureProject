using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Closure2.Models
{
    public class PostModels
    {
        public int ID {set; get;}
        public string text { set; get; }
        [Range(1,5)]
        public int rating {set; get;}
        public int? prodId { set; get; }
        [DataType(DataType.Date)]
        public DateTime postDate { set; get; }

        //public virtual ICollection<CommentModels> Comments { get; set; }
    }

}
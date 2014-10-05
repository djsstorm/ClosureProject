using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Closure2.Models
{
    public class CommentModels
    {
        public int ID { set; get; }
        public string text { set; get; }
        public int? postId { get; set; }
        [DataType(DataType.Date)]
        public DateTime commentDate { set; get; }
    }

   
}
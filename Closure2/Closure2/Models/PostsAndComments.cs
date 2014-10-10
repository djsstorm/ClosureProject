using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Closure2.Models
{
    public class PostsAndComments
    {
        [DisplayName("Comment Text")]
        public string commentText { set; get; }
        [DisplayName("Post Text")]
        public string postText { set; get; }
        public int? prodId { set; get; }
    }
}
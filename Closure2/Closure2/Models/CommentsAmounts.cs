using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Closure2.Models
{
    public class CommentsAmounts
    {
        [DisplayName("Post Text")]
        public string postText { set; get; }
        [DisplayName("Amount of Comments")]
        public int commentsAmount { set; get; }
        public int? prodId { set; get; }
    }
}
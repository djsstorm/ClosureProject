using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Closure2.Models
{
    public class ClosureDbContext : DbContext
    {
        public DbSet<CommentModels> Comments { get; set; }
        public DbSet<PostModels> Posts { get; set; }
        public DbSet<ProductModels> Products { get; set; }
        public DbSet<Branch> Branches { set; get; }
    }
}
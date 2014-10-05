namespace Closure2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedParam : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CommentModels", "commentDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.CommentModels", "postDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CommentModels", "postDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.CommentModels", "commentDate");
        }
    }
}

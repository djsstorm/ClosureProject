namespace Closure2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CommentModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        text = c.String(),
                        postId = c.Int(),
                        postDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PostModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        text = c.String(),
                        rating = c.Int(nullable: false),
                        prodId = c.Int(),
                        postDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ProductModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ProductModels");
            DropTable("dbo.PostModels");
            DropTable("dbo.CommentModels");
        }
    }
}

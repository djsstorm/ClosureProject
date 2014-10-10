namespace Closure2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedBranch : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Branches",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Street = c.String(),
                        latitude = c.Int(nullable: false),
                        longitude = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.ProductModels", "Branch_ID", c => c.Int());
            AddForeignKey("dbo.ProductModels", "Branch_ID", "dbo.Branches", "ID");
            CreateIndex("dbo.ProductModels", "Branch_ID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.ProductModels", new[] { "Branch_ID" });
            DropForeignKey("dbo.ProductModels", "Branch_ID", "dbo.Branches");
            DropColumn("dbo.ProductModels", "Branch_ID");
            DropTable("dbo.Branches");
        }
    }
}

namespace Closure2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedCoordToFloat : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Branches", "latitude", c => c.Single(nullable: false));
            AlterColumn("dbo.Branches", "longitude", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Branches", "longitude", c => c.Int(nullable: false));
            AlterColumn("dbo.Branches", "latitude", c => c.Int(nullable: false));
        }
    }
}

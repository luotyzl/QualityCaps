namespace QualityCaps.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcolumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "IsAvailable", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "IsAvailable");
        }
    }
}

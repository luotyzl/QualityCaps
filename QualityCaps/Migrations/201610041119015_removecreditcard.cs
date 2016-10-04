namespace QualityCaps.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removecreditcard : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Orders", "Experation");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "Experation", c => c.DateTime(nullable: false));
        }
    }
}

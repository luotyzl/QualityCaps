namespace QualityCaps.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Phone", c => c.Binary());

        }

        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Phone");
        }
    }
}

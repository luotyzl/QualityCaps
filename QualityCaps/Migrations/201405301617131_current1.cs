using System.Data.Entity.Migrations;

namespace QualityCaps.Migrations
{
    public partial class current1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Experation", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "Experation");
        }
    }
}

using System.Data.Entity.Migrations;

namespace QualityCaps.Migrations
{
    public partial class second : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Items", "Title", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Items", "Title");
        }
    }
}

using System.Data.Entity.Migrations;

namespace QualityCaps.Migrations
{
    public partial class currentv19 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Items", "Title");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Items", "Title", c => c.String());
        }
    }
}

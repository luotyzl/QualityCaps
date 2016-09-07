using System.Data.Entity.Migrations;

namespace QualityCaps.Migrations
{
    public partial class vCurrent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Items", "InternalImage", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Items", "InternalImage");
        }
    }
}

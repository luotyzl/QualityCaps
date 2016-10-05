namespace QualityCaps.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changecolumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Items", "Image", c => c.Binary());
            DropColumn("dbo.Items", "InternalImage");
            DropColumn("dbo.Items", "ItemPictureUrl");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Items", "ItemPictureUrl", c => c.String(maxLength: 1024));
            AddColumn("dbo.Items", "InternalImage", c => c.Binary());
            DropColumn("dbo.Items", "Image");
        }
    }
}

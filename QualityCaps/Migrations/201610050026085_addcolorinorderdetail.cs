namespace QualityCaps.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcolorinorderdetail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderDetails", "Color", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderDetails", "Color");
        }
    }
}

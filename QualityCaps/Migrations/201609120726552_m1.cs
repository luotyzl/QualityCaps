namespace QualityCaps.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Items",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    CatagorieId = c.Int(nullable: false),
                    SupplierId = c.Int(nullable: false),
                    Name = c.String(nullable: false, maxLength: 160),
                    Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    ItemPictureUrl = c.String(maxLength: 1024),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Catagories", t => t.CatagorieId, cascadeDelete: true)
                .Index(t => t.CatagorieId)
                .ForeignKey("dbo.Suppliers", t => t.SupplierId, cascadeDelete: true)
                .Index(t => t.SupplierId);


        }

        public override void Down()
        {
        }
    }
}

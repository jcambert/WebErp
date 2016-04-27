namespace WebErp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Article");
            CreateTable(
                "dbo.PartTrees",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        Societe = c.Int(nullable: false),
                        Code = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PartItems",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PartItem_Id = c.Guid(),
                        PartTree_ID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PartItems", t => t.PartItem_Id)
                .ForeignKey("dbo.PartTrees", t => t.PartTree_ID)
                .Index(t => t.PartItem_Id)
                .Index(t => t.PartTree_ID);
            
            CreateTable(
                "dbo.PartProperties",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PropertyType = c.Int(nullable: false),
                        StringValue = c.String(),
                        PartItem_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PartItems", t => t.PartItem_Id)
                .Index(t => t.PartItem_Id);
            
            AlterColumn("dbo.Article", "Code", c => c.String());
            AlterColumn("dbo.Article", "ID", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Article", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PartItems", "PartTree_ID", "dbo.PartTrees");
            DropForeignKey("dbo.PartProperties", "PartItem_Id", "dbo.PartItems");
            DropForeignKey("dbo.PartItems", "PartItem_Id", "dbo.PartItems");
            DropIndex("dbo.PartProperties", new[] { "PartItem_Id" });
            DropIndex("dbo.PartItems", new[] { "PartTree_ID" });
            DropIndex("dbo.PartItems", new[] { "PartItem_Id" });
            DropPrimaryKey("dbo.Article");
            AlterColumn("dbo.Article", "ID", c => c.String());
            AlterColumn("dbo.Article", "Code", c => c.String(nullable: false, maxLength: 128));
            DropTable("dbo.PartProperties");
            DropTable("dbo.PartItems");
            DropTable("dbo.PartTrees");
            AddPrimaryKey("dbo.Article", new[] { "Societe", "Code" });
        }
    }
}

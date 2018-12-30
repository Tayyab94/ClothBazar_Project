namespace BazarCloth.DataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProductAndCatagoryTableintoDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Catagories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Name = c.String(),
                        Description = c.String(),
                        Catagory_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Catagories", t => t.Catagory_Id)
                .Index(t => t.Catagory_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "Catagory_Id", "dbo.Catagories");
            DropIndex("dbo.Products", new[] { "Catagory_Id" });
            DropTable("dbo.Products");
            DropTable("dbo.Catagories");
        }
    }
}

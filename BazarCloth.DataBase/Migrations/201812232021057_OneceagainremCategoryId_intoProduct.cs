namespace BazarCloth.DataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OneceagainremCategoryId_intoProduct : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "Catagory_Id", "dbo.Catagories");
            DropIndex("dbo.Products", new[] { "Catagory_Id" });
            RenameColumn(table: "dbo.Products", name: "Catagory_Id", newName: "CategoryID");
            AlterColumn("dbo.Products", "CategoryID", c => c.Int(nullable: false));
            CreateIndex("dbo.Products", "CategoryID");
            AddForeignKey("dbo.Products", "CategoryID", "dbo.Catagories", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "CategoryID", "dbo.Catagories");
            DropIndex("dbo.Products", new[] { "CategoryID" });
            AlterColumn("dbo.Products", "CategoryID", c => c.Int());
            RenameColumn(table: "dbo.Products", name: "CategoryID", newName: "Catagory_Id");
            CreateIndex("dbo.Products", "Catagory_Id");
            AddForeignKey("dbo.Products", "Catagory_Id", "dbo.Catagories", "Id");
        }
    }
}

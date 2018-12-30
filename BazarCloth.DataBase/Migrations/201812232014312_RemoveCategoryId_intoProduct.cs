namespace BazarCloth.DataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveCategoryId_intoProduct : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Products", "CategoryID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "CategoryID", c => c.Int(nullable: false));
        }
    }
}

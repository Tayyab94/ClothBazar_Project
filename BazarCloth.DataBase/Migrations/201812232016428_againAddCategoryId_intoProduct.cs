namespace BazarCloth.DataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class againAddCategoryId_intoProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "CategoryID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "CategoryID");
        }
    }
}

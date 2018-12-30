namespace BazarCloth.DataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsFeaturedPropertyIntoCatagoryTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Catagories", "isFeatured", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Catagories", "isFeatured");
        }
    }
}

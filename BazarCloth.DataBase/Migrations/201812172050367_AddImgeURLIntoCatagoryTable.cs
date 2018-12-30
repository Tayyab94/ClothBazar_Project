namespace BazarCloth.DataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImgeURLIntoCatagoryTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Catagories", "ImageUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Catagories", "ImageUrl");
        }
    }
}

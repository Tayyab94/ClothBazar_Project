namespace BazarCloth.DataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImageUrlProperty_Into_ProductTabale : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "ImageUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "ImageUrl");
        }
    }
}

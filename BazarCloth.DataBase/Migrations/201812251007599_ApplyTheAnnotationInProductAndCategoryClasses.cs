namespace BazarCloth.DataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplyTheAnnotationInProductAndCategoryClasses : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Catagories", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Catagories", "Description", c => c.String(maxLength: 50));
            AlterColumn("dbo.Products", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Products", "Description", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "Description", c => c.String());
            AlterColumn("dbo.Products", "Name", c => c.String());
            AlterColumn("dbo.Catagories", "Description", c => c.String());
            AlterColumn("dbo.Catagories", "Name", c => c.String());
        }
    }
}

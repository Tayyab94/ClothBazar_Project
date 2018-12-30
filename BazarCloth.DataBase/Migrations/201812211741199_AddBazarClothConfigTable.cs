namespace BazarCloth.DataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBazarClothConfigTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BazarClothConfigs",
                c => new
                    {
                        SKey = c.String(nullable: false, maxLength: 128),
                        SValue = c.String(),
                    })
                .PrimaryKey(t => t.SKey);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.BazarClothConfigs");
        }
    }
}

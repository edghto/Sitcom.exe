namespace Sitcoms.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNotNullConstraintToSitcomColumnOfEpisodesTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Episodes", "Sitcom_Id", "dbo.Sitcoms");
            DropIndex("dbo.Episodes", new[] { "Sitcom_Id" });
            AlterColumn("dbo.Episodes", "Sitcom_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Episodes", "Sitcom_Id");
            AddForeignKey("dbo.Episodes", "Sitcom_Id", "dbo.Sitcoms", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Episodes", "Sitcom_Id", "dbo.Sitcoms");
            DropIndex("dbo.Episodes", new[] { "Sitcom_Id" });
            AlterColumn("dbo.Episodes", "Sitcom_Id", c => c.Int());
            CreateIndex("dbo.Episodes", "Sitcom_Id");
            AddForeignKey("dbo.Episodes", "Sitcom_Id", "dbo.Sitcoms", "Id");
        }
    }
}

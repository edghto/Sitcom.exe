namespace Sitcoms.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Episodes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        PremiereDate = c.DateTime(nullable: false),
                        Season = c.Int(nullable: false),
                        Watched = c.Boolean(nullable: false),
                        Sitcom_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sitcoms", t => t.Sitcom_Id)
                .Index(t => t.Sitcom_Id);
            
            CreateTable(
                "dbo.Sitcoms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Episodes", "Sitcom_Id", "dbo.Sitcoms");
            DropIndex("dbo.Episodes", new[] { "Sitcom_Id" });
            DropTable("dbo.Sitcoms");
            DropTable("dbo.Episodes");
        }
    }
}

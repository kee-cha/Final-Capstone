namespace FinalCapstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Comment = c.String(),
                        ClientId = c.Int(nullable: false),
                        TherapistId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .ForeignKey("dbo.MassageTherapists", t => t.TherapistId, cascadeDelete: true)
                .Index(t => t.ClientId)
                .Index(t => t.TherapistId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reviews", "TherapistId", "dbo.MassageTherapists");
            DropForeignKey("dbo.Reviews", "ClientId", "dbo.Clients");
            DropIndex("dbo.Reviews", new[] { "TherapistId" });
            DropIndex("dbo.Reviews", new[] { "ClientId" });
            DropTable("dbo.Reviews");
        }
    }
}

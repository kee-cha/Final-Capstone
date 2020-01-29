namespace FinalCapstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class models : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClientPrefs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HeadPain = c.Boolean(nullable: false),
                        NeckPain = c.Boolean(nullable: false),
                        UpperBackPain = c.Boolean(nullable: false),
                        LowBackPain = c.Boolean(nullable: false),
                        ShoulderPain = c.Boolean(nullable: false),
                        ArmPain = c.Boolean(nullable: false),
                        WristHandPain = c.Boolean(nullable: false),
                        HipPain = c.Boolean(nullable: false),
                        ThighPain = c.Boolean(nullable: false),
                        KneeLegPain = c.Boolean(nullable: false),
                        AnkleFootPain = c.Boolean(nullable: false),
                        TherapistGender = c.String(),
                        TherapistSpecialty = c.String(),
                        ClientId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .Index(t => t.ClientId);
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Street = c.String(),
                        City = c.String(),
                        State = c.String(),
                        Zip = c.String(),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                        ApplicationId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationId)
                .Index(t => t.ApplicationId);
            
            CreateTable(
                "dbo.ClientTherapists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClientId = c.Int(nullable: false),
                        TherapistId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .ForeignKey("dbo.MassageTherapists", t => t.TherapistId, cascadeDelete: true)
                .Index(t => t.ClientId)
                .Index(t => t.TherapistId);
            
            CreateTable(
                "dbo.MassageTherapists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Street = c.String(),
                        City = c.String(),
                        State = c.String(),
                        Zip = c.String(),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                        Gender = c.String(),
                        Specialty = c.String(),
                        Rating = c.Double(nullable: false),
                        ApplicationId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationId)
                .Index(t => t.ApplicationId);
            
            CreateTable(
                "dbo.Documents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subjective = c.String(),
                        Objective = c.String(),
                        Assessment = c.String(),
                        Plan = c.String(),
                        ClientId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .Index(t => t.ClientId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Documents", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.ClientTherapists", "TherapistId", "dbo.MassageTherapists");
            DropForeignKey("dbo.MassageTherapists", "ApplicationId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ClientTherapists", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.ClientPrefs", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.Clients", "ApplicationId", "dbo.AspNetUsers");
            DropIndex("dbo.Documents", new[] { "ClientId" });
            DropIndex("dbo.MassageTherapists", new[] { "ApplicationId" });
            DropIndex("dbo.ClientTherapists", new[] { "TherapistId" });
            DropIndex("dbo.ClientTherapists", new[] { "ClientId" });
            DropIndex("dbo.Clients", new[] { "ApplicationId" });
            DropIndex("dbo.ClientPrefs", new[] { "ClientId" });
            DropTable("dbo.Documents");
            DropTable("dbo.MassageTherapists");
            DropTable("dbo.ClientTherapists");
            DropTable("dbo.Clients");
            DropTable("dbo.ClientPrefs");
        }
    }
}

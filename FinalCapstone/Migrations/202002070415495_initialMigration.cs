namespace FinalCapstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Appointments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AppointmentDate = c.String(),
                        AppointmentTime = c.String(),
                        ClientId = c.Int(nullable: false),
                        TherapistId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .ForeignKey("dbo.MassageTherapists", t => t.TherapistId, cascadeDelete: true)
                .Index(t => t.ClientId)
                .Index(t => t.TherapistId);
            
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
                        Latitude = c.Double(),
                        Longitude = c.Double(),
                        ApplicationId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationId)
                .Index(t => t.ApplicationId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
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
                        TimeFramePref = c.String(),
                        SessionPerDay = c.Int(nullable: false),
                        AppointmentDate = c.String(),
                        Schedule1 = c.String(),
                        Schedule2 = c.String(),
                        Schedule3 = c.String(),
                        Schedule4 = c.String(),
                        IsOpen1 = c.Boolean(nullable: false),
                        IsOpen2 = c.Boolean(nullable: false),
                        IsOpen3 = c.Boolean(nullable: false),
                        IsOpen4 = c.Boolean(nullable: false),
                        ApplicationId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationId)
                .Index(t => t.ApplicationId);
            
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
                        TimeFramePref = c.String(),
                        AppointmentTime = c.String(),
                        AppointmentDate = c.String(),
                        ClientId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .Index(t => t.ClientId);
            
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
                "dbo.Documents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subjective = c.String(nullable: false),
                        Objective = c.String(nullable: false),
                        Assessment = c.String(nullable: false),
                        Plan = c.String(nullable: false),
                        ClientId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .Index(t => t.ClientId);
            
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
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Reviews", "TherapistId", "dbo.MassageTherapists");
            DropForeignKey("dbo.Reviews", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.Documents", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.ClientTherapists", "TherapistId", "dbo.MassageTherapists");
            DropForeignKey("dbo.ClientTherapists", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.ClientPrefs", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.Appointments", "TherapistId", "dbo.MassageTherapists");
            DropForeignKey("dbo.MassageTherapists", "ApplicationId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Appointments", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.Clients", "ApplicationId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Reviews", new[] { "TherapistId" });
            DropIndex("dbo.Reviews", new[] { "ClientId" });
            DropIndex("dbo.Documents", new[] { "ClientId" });
            DropIndex("dbo.ClientTherapists", new[] { "TherapistId" });
            DropIndex("dbo.ClientTherapists", new[] { "ClientId" });
            DropIndex("dbo.ClientPrefs", new[] { "ClientId" });
            DropIndex("dbo.MassageTherapists", new[] { "ApplicationId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Clients", new[] { "ApplicationId" });
            DropIndex("dbo.Appointments", new[] { "TherapistId" });
            DropIndex("dbo.Appointments", new[] { "ClientId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Reviews");
            DropTable("dbo.Documents");
            DropTable("dbo.ClientTherapists");
            DropTable("dbo.ClientPrefs");
            DropTable("dbo.MassageTherapists");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Clients");
            DropTable("dbo.Appointments");
        }
    }
}

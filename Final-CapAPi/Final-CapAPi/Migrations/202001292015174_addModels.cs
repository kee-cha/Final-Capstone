namespace Final_CapAPi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Causes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        InjuryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Injuries", t => t.InjuryId, cascadeDelete: true)
                .Index(t => t.InjuryId);
            
            CreateTable(
                "dbo.Injuries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        InjuryLocation = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Treatments",
                c => new
                    {
                        Description = c.String(nullable: false, maxLength: 128),
                        InjuryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Description)
                .ForeignKey("dbo.Injuries", t => t.InjuryId, cascadeDelete: true)
                .Index(t => t.InjuryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Treatments", "InjuryId", "dbo.Injuries");
            DropForeignKey("dbo.Causes", "InjuryId", "dbo.Injuries");
            DropIndex("dbo.Treatments", new[] { "InjuryId" });
            DropIndex("dbo.Causes", new[] { "InjuryId" });
            DropTable("dbo.Treatments");
            DropTable("dbo.Injuries");
            DropTable("dbo.Causes");
        }
    }
}

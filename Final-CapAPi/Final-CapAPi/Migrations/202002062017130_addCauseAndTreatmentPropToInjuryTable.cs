namespace Final_CapAPi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCauseAndTreatmentPropToInjuryTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Injuries", "Cause", c => c.String());
            AddColumn("dbo.Injuries", "Treatment", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Injuries", "Treatment");
            DropColumn("dbo.Injuries", "Cause");
        }
    }
}

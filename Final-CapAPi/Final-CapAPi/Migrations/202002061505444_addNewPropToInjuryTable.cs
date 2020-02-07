namespace Final_CapAPi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addNewPropToInjuryTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Injuries", "Type", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Injuries", "Type");
        }
    }
}

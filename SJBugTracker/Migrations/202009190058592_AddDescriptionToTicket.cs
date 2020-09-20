namespace SJBugTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDescriptionToTicket : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tickets", "Name", c => c.String());
            AddColumn("dbo.Tickets", "Description", c => c.String());
            DropColumn("dbo.Tickets", "Title");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tickets", "Title", c => c.String());
            DropColumn("dbo.Tickets", "Description");
            DropColumn("dbo.Tickets", "Name");
        }
    }
}

namespace SJBugTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateTicketPriorities : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO TicketPriorities (Name) VALUES ('High')");
            Sql("INSERT INTO TicketPriorities (Name) VALUES ('Medium')");
            Sql("INSERT INTO TicketPriorities (Name) VALUES ('Low')");
            Sql("INSERT INTO TicketPriorities (Name) VALUES ('None')");
        }
        
        public override void Down()
        {
        }
    }
}

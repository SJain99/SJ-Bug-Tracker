namespace SJBugTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateTicketStatus : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO TicketStatus (Name) VALUES ('Open')");
            Sql("INSERT INTO TicketStatus (Name) VALUES ('In Progress')");
            Sql("INSERT INTO TicketStatus (Name) VALUES ('Resolved')");
            Sql("INSERT INTO TicketStatus (Name) VALUES ('Closed')");
            Sql("INSERT INTO TicketStatus (Name) VALUES ('Reopened')");
        }
        
        public override void Down()
        {
        }
    }
}

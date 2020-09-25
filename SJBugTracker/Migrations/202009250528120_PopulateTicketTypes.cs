namespace SJBugTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateTicketTypes : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO TicketTypes (Name) VALUES ('Bug/Error')");
            Sql("INSERT INTO TicketTypes (Name) VALUES ('Feature Request')");
            Sql("INSERT INTO TicketTypes (Name) VALUES ('Training/Document Request')");
            Sql("INSERT INTO TicketTypes (Name) VALUES ('Other')");
        }
        
        public override void Down()
        {
        }
    }
}

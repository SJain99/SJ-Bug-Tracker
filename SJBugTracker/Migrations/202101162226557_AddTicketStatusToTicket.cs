namespace SJBugTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTicketStatusToTicket : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TicketStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Tickets", "TicketStatusId", c => c.Int(nullable: false));
            CreateIndex("dbo.Tickets", "TicketStatusId");
            AddForeignKey("dbo.Tickets", "TicketStatusId", "dbo.TicketStatus", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tickets", "TicketStatusId", "dbo.TicketStatus");
            DropIndex("dbo.Tickets", new[] { "TicketStatusId" });
            DropColumn("dbo.Tickets", "TicketStatusId");
            DropTable("dbo.TicketStatus");
        }
    }
}

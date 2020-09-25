namespace SJBugTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTicketTypeToTicket : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TicketTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Tickets", "TicketTypeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Tickets", "TicketTypeId");
            AddForeignKey("dbo.Tickets", "TicketTypeId", "dbo.TicketTypes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tickets", "TicketTypeId", "dbo.TicketTypes");
            DropIndex("dbo.Tickets", new[] { "TicketTypeId" });
            DropColumn("dbo.Tickets", "TicketTypeId");
            DropTable("dbo.TicketTypes");
        }
    }
}

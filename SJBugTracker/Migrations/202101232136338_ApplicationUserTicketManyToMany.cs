namespace SJBugTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplicationUserTicketManyToMany : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplicationUserTickets",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        Ticket_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.Ticket_Id })
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .ForeignKey("dbo.Tickets", t => t.Ticket_Id, cascadeDelete: true)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.Ticket_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApplicationUserTickets", "Ticket_Id", "dbo.Tickets");
            DropForeignKey("dbo.ApplicationUserTickets", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ApplicationUserTickets", new[] { "Ticket_Id" });
            DropIndex("dbo.ApplicationUserTickets", new[] { "ApplicationUser_Id" });
            DropTable("dbo.ApplicationUserTickets");
        }
    }
}

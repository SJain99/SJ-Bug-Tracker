namespace SJBugTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDataAnnotationsToDomainClasses : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Projects", "Name", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Tickets", "Name", c => c.String(nullable: false, maxLength: 255));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tickets", "Name", c => c.String());
            AlterColumn("dbo.Projects", "Name", c => c.String());
        }
    }
}

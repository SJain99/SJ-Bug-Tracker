namespace SJBugTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateUnassignedRoleForNewUsers : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO[dbo].[AspNetRoles] ([Id], [Name]) VALUES(N'6b8ce7f4-726d-4fc7-8fd9-2e4f09b9e066', N'Unassigned')");
        }
        
        public override void Down()
        {
        }
    }
}

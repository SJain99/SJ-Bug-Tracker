namespace SJBugTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedRoles : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'a4c470ea-425f-4a98-83c2-69f495e6e501', N'saranshjain99@gmail.com', 0, N'AB1jDbzDNl482pMTAtkak4dnJMwhH+H6REjeRO/6jHlcIoFhWvybMQFw7buW+ED1Sg==', N'2935fb1e-c90c-4097-9b35-b070e0fc717f', NULL, 0, 0, NULL, 1, 0, N'saranshjain99@gmail.com')");
            Sql("INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'759a9587-8b75-4209-a5eb-3f4e57e3a3fc', N'Admin')");
            Sql("INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'dc581abb-7c4a-4800-8e50-7b65d0e764aa', N'Developer')");
            Sql("INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'8c33b902-559c-4dfb-ad15-245674d1799a', N'Project Manager')");
            Sql("INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'7187a739-a89a-43ff-af3d-aa882c531598', N'Ticket Submitter')");
            Sql("INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'a4c470ea-425f-4a98-83c2-69f495e6e501', N'759a9587-8b75-4209-a5eb-3f4e57e3a3fc')");
        }
        
        public override void Down()
        {
        }
    }
}

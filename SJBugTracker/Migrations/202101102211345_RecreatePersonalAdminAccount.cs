namespace SJBugTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RecreatePersonalAdminAccount : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'b7f6018f-7d3c-4bdc-ae3d-cb47f554bc51', N'saranshjain99@gmail.com', 0, N'AHGGdt6xnPps9EKzfBcWODZI/27N3Z+peFxPZpuJPTFH8PvZ4Xc0zrdHmYBBv/6lgw==', N'1ee26b36-2b0c-48e6-80d8-625e6e8f540e', NULL, 0, 0, NULL, 1, 0, N'saranshjain99@gmail.com')");
            Sql("INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'b7f6018f-7d3c-4bdc-ae3d-cb47f554bc51', N'759a9587-8b75-4209-a5eb-3f4e57e3a3fc')");
        }
        
        public override void Down()
        {
        }
    }
}

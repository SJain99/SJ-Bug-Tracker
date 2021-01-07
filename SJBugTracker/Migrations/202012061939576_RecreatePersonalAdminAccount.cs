namespace SJBugTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RecreatePersonalAdminAccount : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO[dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES(N'a8261e2b-c616-4545-94d6-08e2fe8b9f9b', N'saranshjain99@gmail.com', 0, N'AEL8DcBMi9WJTDX4RvW5d7eoOfdTJJU3iEFE8HMODunMlZjoSQoKyzbOm4HVAPvmqg==', N'3819c719-42b3-4704-a753-78eeb06c087a', NULL, 0, 0, NULL, 1, 0, N'saranshjain99@gmail.com')");
            Sql("INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'a8261e2b-c616-4545-94d6-08e2fe8b9f9b', N'759a9587-8b75-4209-a5eb-3f4e57e3a3fc')");
        }

        public override void Down()
        {
        }
    }
}

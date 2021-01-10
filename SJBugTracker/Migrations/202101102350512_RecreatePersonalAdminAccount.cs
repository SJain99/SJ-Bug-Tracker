namespace SJBugTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RecreatePersonalAdminAccount : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'32c5d0e8-eb24-4650-a07d-ed3d1ff92583', N'saranshjain99@gmail.com', 0, N'AOOrfbV9br6WinrzCuE5khV4yky1HM1NApMGsrIbxY/dBEzkxNHoLipyphDKfw74Bg==', N'61ee0d88-a11b-4851-94db-fbb22c33b395', NULL, 0, 0, NULL, 1, 0, N'saranshjain99@gmail.com')");
            Sql("INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'32c5d0e8-eb24-4650-a07d-ed3d1ff92583', N'759a9587-8b75-4209-a5eb-3f4e57e3a3fc')");
        }
        
        public override void Down()
        {
        }
    }
}

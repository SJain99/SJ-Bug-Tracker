namespace SJBugTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixUserNameError : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE [dbo].[AspNetUsers] SET UserName = 'saranshjain99@gmail.com' WHERE Id = 'a8261e2b-c616-4545-94d6-08e2fe8b9f9b'");
        }
        
        public override void Down()
        {
        }
    }
}

namespace ViFlix.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddColumnsInAspNetUsersTableToMakeItCompatibleWithNetcore : DbMigration
    {
        public override void Up()
        {
            Sql(@"ALTER TABLE [ViFlix].[dbo].[AspNetUsers]
                    ADD ConcurrencyStamp NVARCHAR(max),
                    LockoutEnd datetime,
	                NormalizedEmail VARCHAR(50),
	                NormalizedUserName VARCHAR(40);");
        }

        public override void Down()
        {
        }
    }
}

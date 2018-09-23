namespace ViFlix.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class SeedYiannisUser : DbMigration
    {
        public override void Up()
        {
            Sql(@"INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [ConcurrencyStamp], [LockoutEnd], [NormalizedEmail], [NormalizedUserName]) 
                    VALUES (N'df37c3f4-4772-4312-b34a-e8dc4ff3e092', N'yiannis.ioannidis@hotmail.gr', 0, N'AQAAAAEAACcQAAAAEEedr1tz1Tv3B8qW5UokmQaPAuEZmb01amhNW0F694A+rtt+Cdpuy64H1yRz75Efcw==', N'2PEPX7ET5WTSMXYJI7VJ4EXRD6C7WGTY', NULL, 0, 0, NULL, 0, 0, N'yiannis.ioannidis@hotmail.gr', NULL, NULL, N'yiannis.ioannidis@hotmail.gr', NULL)");
        }

        public override void Down()
        {
        }
    }
}

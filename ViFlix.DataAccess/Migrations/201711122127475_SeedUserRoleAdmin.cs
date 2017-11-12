namespace ViFlix.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class SeedUserRoleAdmin : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'f597d752-93c2-4a35-807c-506c244036b5', N'admin@viflix.gr', 0, N'AJU1SwYzvgjP/0DL50s8yAWAhe0NWQDbAP8NBuvrLNbygfCoZVdQHmzvqrYVOm3/GA==', N'a319abbf-559a-452f-8d31-f6f21943bdee', NULL, 0, 0, NULL, 0, 0, N'admin@viflix.gr')
                
                INSERT INTO [dbo].[AspNetRoles] ([Id], [Name], [Discriminator]) VALUES (N'b870dd28-597d-4192-afb6-06208be61208', N'admin', N'AppRole')

                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'f597d752-93c2-4a35-807c-506c244036b5', N'b870dd28-597d-4192-afb6-06208be61208')
            ");
        }

        public override void Down()
        {
        }
    }
}

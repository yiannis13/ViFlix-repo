namespace ViFlix.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class FillNormalizedEmailColumnWithAdminEmail : DbMigration
    {
        public override void Up()
        {
            Sql(@"UPDATE [ViFlix].[dbo].[AspNetUsers] 
                SET normalizedEmail = 'admin@viflix.gr'
                WHERE Email = 'admin@viflix.gr'");
        }

        public override void Down()
        {
        }
    }
}

namespace ViFlix.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PopulateMembershipTypeNames : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE dbo.MembershipTypes SET Name = 'Pay As You Go' where id=1");
            Sql("UPDATE dbo.MembershipTypes SET Name = 'Monthly' where id=2");
            Sql("UPDATE dbo.MembershipTypes SET Name = 'Quarterly' where id=3");
            Sql("UPDATE dbo.MembershipTypes SET Name = 'Annual' where id=4");
        }

        public override void Down()
        {
        }
    }
}

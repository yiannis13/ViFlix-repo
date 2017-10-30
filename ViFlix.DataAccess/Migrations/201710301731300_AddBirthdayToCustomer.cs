namespace ViFlix.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddBirthdayToCustomer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "Birthday", c => c.DateTime());
            Sql("UPDATE dbo.Customers SET Birthday = CAST('05/13/1983' AS DATETIME) where id=1");
        }

        public override void Down()
        {
            DropColumn("dbo.Customers", "Birthday");
        }
    }
}

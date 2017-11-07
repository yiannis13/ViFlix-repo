namespace ViFlix.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddNumberAvailableAndDateToBeReturnedColumnsToRental : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "NumberAvailable", c => c.Int(nullable: false));
            AddColumn("dbo.Rentals", "DateToBeReturned", c => c.DateTime(nullable: false));

            Sql("UPDATE Movies SET NumberAvailable = NumberInStock");
        }

        public override void Down()
        {
            DropColumn("dbo.Rentals", "DateToBeReturned");
            DropColumn("dbo.Movies", "NumberAvailable");
        }
    }
}

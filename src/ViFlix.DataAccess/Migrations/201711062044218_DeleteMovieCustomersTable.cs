namespace ViFlix.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteMovieCustomersTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MovieCustomers", "Movie_Id", "dbo.Movies");
            DropForeignKey("dbo.MovieCustomers", "Customer_Id", "dbo.Customers");
            DropIndex("dbo.MovieCustomers", new[] { "Movie_Id" });
            DropIndex("dbo.MovieCustomers", new[] { "Customer_Id" });
            DropTable("dbo.MovieCustomers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.MovieCustomers",
                c => new
                    {
                        Movie_Id = c.Int(nullable: false),
                        Customer_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Movie_Id, t.Customer_Id });
            
            CreateIndex("dbo.MovieCustomers", "Customer_Id");
            CreateIndex("dbo.MovieCustomers", "Movie_Id");
            AddForeignKey("dbo.MovieCustomers", "Customer_Id", "dbo.Customers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.MovieCustomers", "Movie_Id", "dbo.Movies", "Id", cascadeDelete: true);
        }
    }
}

using System;
using ViFlix.DataAccess.Entities;

namespace ViFlix.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddGenreTableAndPolulate : DbMigration
    {
        private const string FkName = "FKMovie_MovieGenre";

        public override void Up()
        {
            // Create the lookup table
            CreateTable(typeof(Genre).Name, t =>
                new
                {
                    ID = t.Int(nullable: false),
                    GenreName = t.String()
                }
            );

            // Set ID as the primary key
            AddPrimaryKey(typeof(Genre).Name, "ID");

            // Populate the table with enum values
            foreach (Genre genre in Enum.GetValues(typeof(Genre)))
            {
                Sql("INSERT INTO " +
                    typeof(Genre).Name +
                    " SELECT " + (int)genre + ", '" + genre.ToString() + "'");
            }

            // Add the FK to Movie table
            //AddForeignKey("db.Movies", "Genre",
            //    typeof(Genre).Name, "ID", name: FkName);
        }

        public override void Down()
        {
            // Delete the FK from Movie table
            //DropForeignKey(typeof(Genre).Name, FkName);

            // Delete the lookup table
            DropTable(typeof(Genre).Name);
        }
    }
}

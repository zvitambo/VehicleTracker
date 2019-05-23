using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace AspAng.Migrations
{
    public partial class seedDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
             migrationBuilder.Sql("INSERT INTO Makes (Name) VALUES ('Make1')");
             migrationBuilder.Sql("INSERT INTO Makes (Name) VALUES ('Make2')");
             migrationBuilder.Sql("INSERT INTO Makes (Name) VALUES ('Make3')");

             migrationBuilder.Sql("INSERT INTO Models (Name , MakeId) VALUES ('Make1-ModelA' , (SELECT Id FROM Makes WHERE name = 'Make1'))");
             migrationBuilder.Sql("INSERT INTO Models (Name , MakeId) VALUES ('Make1-ModelB' , (SELECT Id FROM Makes WHERE name = 'Make1'))");
             migrationBuilder.Sql("INSERT INTO Models (Name , MakeId) VALUES ('Make1-ModelC' , (SELECT Id FROM Makes WHERE name = 'Make1'))");

             migrationBuilder.Sql("INSERT INTO Models (Name , MakeId) VALUES ('Make2-ModelA' , (SELECT Id FROM Makes WHERE name = 'Make2'))");
             migrationBuilder.Sql("INSERT INTO Models (Name , MakeId) VALUES ('Make2-ModelB' , (SELECT Id FROM Makes WHERE name = 'Make2'))");
             migrationBuilder.Sql("INSERT INTO Models (Name , MakeId) VALUES ('Make2-ModelC' , (SELECT Id FROM Makes WHERE name = 'Make2'))");

             migrationBuilder.Sql("INSERT INTO Models (Name , MakeId) VALUES ('Make3-ModelA' , (SELECT Id FROM Makes WHERE name = 'Make3'))");
             migrationBuilder.Sql("INSERT INTO Models (Name , MakeId) VALUES ('Make3-ModelB' , (SELECT Id FROM Makes WHERE name = 'Make3'))");
             migrationBuilder.Sql("INSERT INTO Models (Name , MakeId) VALUES ('Make3-ModelC' , (SELECT Id FROM Makes WHERE name = 'Make3'))");

             migrationBuilder.Sql("INSERT INTO Features (Name) VALUES ('ABS Braking')");
             migrationBuilder.Sql("INSERT INTO Features (Name) VALUES ('Panoramic Roof')");
             migrationBuilder.Sql("INSERT INTO Features (Name) VALUES ('Wifi')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Makes WHERE Name IN ('Make1', 'Make2', 'Make3')");
            migrationBuilder.Sql("DELETE FROM Features WHERE Name IN ('ABS Braking', 'Panoramic Roof', 'Wifi')");
        }
    }
}

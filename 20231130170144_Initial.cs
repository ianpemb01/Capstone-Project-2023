using Microsoft.EntityFrameworkCore.Migrations;

namespace MoldMakingCalculator.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cubes",
                columns: table => new
                {
                    Projectname1 = table.Column<string>(type: "TEXT", nullable: false),
                    Height1 = table.Column<double>(type: "REAL", nullable: false),
                    Width1 = table.Column<double>(type: "REAL", nullable: false),
                    Length1 = table.Column<double>(type: "REAL", nullable: false),
                    Water1 = table.Column<double>(type: "REAL", nullable: false),
                    Plaster1 = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cubes", x => x.Projectname1);
                });

            migrationBuilder.CreateTable(
                name: "Cylinders",
                columns: table => new
                {
                    Projectname2 = table.Column<string>(type: "TEXT", nullable: false),
                    Height2 = table.Column<double>(type: "REAL", nullable: false),
                    Radius2 = table.Column<double>(type: "REAL", nullable: false),
                    Water2 = table.Column<double>(type: "REAL", nullable: false),
                    Plaster2 = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cylinders", x => x.Projectname2);
                });

            migrationBuilder.CreateTable(
                name: "Pyramids",
                columns: table => new
                {
                    Projectname4 = table.Column<string>(type: "TEXT", nullable: false),
                    Height4 = table.Column<double>(type: "REAL", nullable: false),
                    Width4 = table.Column<double>(type: "REAL", nullable: false),
                    Length4 = table.Column<double>(type: "REAL", nullable: false),
                    Water4 = table.Column<double>(type: "REAL", nullable: false),
                    Plaster4 = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pyramids", x => x.Projectname4);
                });

            migrationBuilder.CreateTable(
                name: "Spheres",
                columns: table => new
                {
                    Projectname3 = table.Column<string>(type: "TEXT", nullable: false),
                    Radius3 = table.Column<double>(type: "REAL", nullable: false),
                    Water3 = table.Column<double>(type: "REAL", nullable: false),
                    Plaster3 = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spheres", x => x.Projectname3);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cubes");

            migrationBuilder.DropTable(
                name: "Cylinders");

            migrationBuilder.DropTable(
                name: "Pyramids");

            migrationBuilder.DropTable(
                name: "Spheres");
        }
    }
}

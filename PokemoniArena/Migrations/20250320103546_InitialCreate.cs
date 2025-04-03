using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokemoniArena.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Souboje",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    HracJmeno = table.Column<string>(type: "TEXT", nullable: false),
                    HracZivoty = table.Column<int>(type: "INTEGER", nullable: false),
                    ProtivnikJmeno = table.Column<string>(type: "TEXT", nullable: false),
                    ProtivnikZivoty = table.Column<int>(type: "INTEGER", nullable: false),
                    KonecSouboje = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Souboje", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Souboje");
        }
    }
}

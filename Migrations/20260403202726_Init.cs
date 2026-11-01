using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebQLBH.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NhomSPs",
                columns: table => new
                {
                    MaNSP = table.Column<byte>(type: "tinyint", nullable: false),
                    TenNSP = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhomSPs", x => x.MaNSP);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NhomSPs");
        }
    }
}

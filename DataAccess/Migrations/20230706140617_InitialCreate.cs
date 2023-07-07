using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Emails",
                columns: table => new
                {
                    EmailAddress = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    IsVerified = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emails", x => x.EmailAddress);
                });

            migrationBuilder.CreateTable(
                name: "FutureMails",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmailAddress = table.Column<string>(type: "varchar(255)", nullable: true),
                    Text = table.Column<string>(type: "nvarchar(max)", maxLength: 10000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FutureMails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FutureMails_Emails_EmailAddress",
                        column: x => x.EmailAddress,
                        principalTable: "Emails",
                        principalColumn: "EmailAddress");
                });

            migrationBuilder.CreateIndex(
                name: "IX_FutureMails_EmailAddress",
                table: "FutureMails",
                column: "EmailAddress");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FutureMails");

            migrationBuilder.DropTable(
                name: "Emails");
        }
    }
}

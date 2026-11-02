using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelpDesk.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddUserTicketRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TicketOlusturanId",
                table: "Tickets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdSoyad = table.Column<string>(type: "varchar(50)", nullable: false),
                    Email = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_TicketOlusturanId",
                table: "Tickets",
                column: "TicketOlusturanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Users_TicketOlusturanId",
                table: "Tickets",
                column: "TicketOlusturanId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Users_TicketOlusturanId",
                table: "Tickets");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_TicketOlusturanId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "TicketOlusturanId",
                table: "Tickets");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PostSystem.Data.Migrations
{
    public partial class Initial_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created_On = table.Column<DateTime>(nullable: false),
                    Updated_On = table.Column<DateTime>(nullable: false),
                    City_Name = table.Column<string>(nullable: true),
                    City_Post_Code = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created_On = table.Column<DateTime>(nullable: false),
                    Updated_On = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Width = table.Column<double>(nullable: false),
                    Length = table.Column<double>(nullable: false),
                    Height = table.Column<double>(nullable: false),
                    Weight = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PostOffices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created_On = table.Column<DateTime>(nullable: false),
                    Updated_On = table.Column<DateTime>(nullable: false),
                    City_Id = table.Column<int>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    Desk_Count = table.Column<short>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostOffices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostOffices_Cities_City_Id",
                        column: x => x.City_Id,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Deliveries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created_On = table.Column<DateTime>(nullable: false),
                    Updated_On = table.Column<DateTime>(nullable: false),
                    Mail_Id = table.Column<int>(nullable: false),
                    From_Office_Id = table.Column<int>(nullable: false),
                    To_Office_Id = table.Column<int>(nullable: false),
                    Details = table.Column<string>(nullable: true),
                    Tax = table.Column<double>(nullable: false),
                    Express_Delivery = table.Column<bool>(nullable: false),
                    Delivery_MailId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deliveries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Deliveries_Mails_Delivery_MailId",
                        column: x => x.Delivery_MailId,
                        principalTable: "Mails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Deliveries_PostOffices_From_Office_Id",
                        column: x => x.From_Office_Id,
                        principalTable: "PostOffices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Deliveries_PostOffices_To_Office_Id",
                        column: x => x.To_Office_Id,
                        principalTable: "PostOffices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_Delivery_MailId",
                table: "Deliveries",
                column: "Delivery_MailId");

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_From_Office_Id",
                table: "Deliveries",
                column: "From_Office_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_To_Office_Id",
                table: "Deliveries",
                column: "To_Office_Id");

            migrationBuilder.CreateIndex(
                name: "IX_PostOffices_City_Id",
                table: "PostOffices",
                column: "City_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Deliveries");

            migrationBuilder.DropTable(
                name: "Mails");

            migrationBuilder.DropTable(
                name: "PostOffices");

            migrationBuilder.DropTable(
                name: "Cities");
        }
    }
}

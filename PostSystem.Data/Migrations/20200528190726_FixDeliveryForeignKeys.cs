using Microsoft.EntityFrameworkCore.Migrations;

namespace PostSystem.Data.Migrations
{
    public partial class FixDeliveryForeignKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deliveries_Mails_Delivery_MailId",
                table: "Deliveries");

            migrationBuilder.DropIndex(
                name: "IX_Deliveries_Delivery_MailId",
                table: "Deliveries");

            migrationBuilder.DropColumn(
                name: "Delivery_MailId",
                table: "Deliveries");

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_Mail_Id",
                table: "Deliveries",
                column: "Mail_Id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Deliveries_Mails_Mail_Id",
                table: "Deliveries",
                column: "Mail_Id",
                principalTable: "Mails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deliveries_Mails_Mail_Id",
                table: "Deliveries");

            migrationBuilder.DropIndex(
                name: "IX_Deliveries_Mail_Id",
                table: "Deliveries");

            migrationBuilder.AddColumn<int>(
                name: "Delivery_MailId",
                table: "Deliveries",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_Delivery_MailId",
                table: "Deliveries",
                column: "Delivery_MailId");

            migrationBuilder.AddForeignKey(
                name: "FK_Deliveries_Mails_Delivery_MailId",
                table: "Deliveries",
                column: "Delivery_MailId",
                principalTable: "Mails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

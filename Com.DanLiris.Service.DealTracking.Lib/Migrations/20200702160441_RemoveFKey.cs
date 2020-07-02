using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.DanLiris.Service.DealTracking.Lib.Migrations
{
    public partial class RemoveFKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DealTrackingDeals_DealTrackingStages_StageId",
                table: "DealTrackingDeals");

            migrationBuilder.DropIndex(
                name: "IX_DealTrackingDeals_StageId",
                table: "DealTrackingDeals");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_DealTrackingDeals_StageId",
                table: "DealTrackingDeals",
                column: "StageId");

            migrationBuilder.AddForeignKey(
                name: "FK_DealTrackingDeals_DealTrackingStages_StageId",
                table: "DealTrackingDeals",
                column: "StageId",
                principalTable: "DealTrackingStages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

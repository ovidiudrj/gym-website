using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymWebApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddSubscription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_MembershipPlans_MembershipPlanId1",
                table: "Subscriptions");

            migrationBuilder.DropIndex(
                name: "IX_Subscriptions_MembershipPlanId1",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "MembershipPlanId1",
                table: "Subscriptions");

            migrationBuilder.AlterColumn<int>(
                name: "MembershipPlanId",
                table: "Subscriptions",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_MembershipPlanId",
                table: "Subscriptions",
                column: "MembershipPlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_MembershipPlans_MembershipPlanId",
                table: "Subscriptions",
                column: "MembershipPlanId",
                principalTable: "MembershipPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_MembershipPlans_MembershipPlanId",
                table: "Subscriptions");

            migrationBuilder.DropIndex(
                name: "IX_Subscriptions_MembershipPlanId",
                table: "Subscriptions");

            migrationBuilder.AlterColumn<string>(
                name: "MembershipPlanId",
                table: "Subscriptions",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "MembershipPlanId1",
                table: "Subscriptions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_MembershipPlanId1",
                table: "Subscriptions",
                column: "MembershipPlanId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_MembershipPlans_MembershipPlanId1",
                table: "Subscriptions",
                column: "MembershipPlanId1",
                principalTable: "MembershipPlans",
                principalColumn: "Id");
        }
    }
}

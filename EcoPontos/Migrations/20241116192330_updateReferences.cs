using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoPontos.Migrations
{
    /// <inheritdoc />
    public partial class updateReferences : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_TaskRegister_GS_TB_TaskWork_GS_TaskId",
                table: "TB_TaskRegister_GS");

            migrationBuilder.DropForeignKey(
                name: "FK_TB_TaskRegister_GS_TB_User_GS_UserId",
                table: "TB_TaskRegister_GS");

            migrationBuilder.DropIndex(
                name: "IX_TB_TaskRegister_GS_TaskId",
                table: "TB_TaskRegister_GS");

            migrationBuilder.DropColumn(
                name: "TaskId",
                table: "TB_TaskRegister_GS");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "TB_TaskRegister_GS",
                type: "NUMBER(10)",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "NUMBER(10)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TaskWorkId",
                table: "TB_TaskRegister_GS",
                type: "NUMBER(10)",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TB_TaskRegister_GS_TaskWorkId",
                table: "TB_TaskRegister_GS",
                column: "TaskWorkId");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_TaskRegister_GS_TB_TaskWork_GS_TaskWorkId",
                table: "TB_TaskRegister_GS",
                column: "TaskWorkId",
                principalTable: "TB_TaskWork_GS",
                principalColumn: "TaskWorkId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TB_TaskRegister_GS_TB_User_GS_UserId",
                table: "TB_TaskRegister_GS",
                column: "UserId",
                principalTable: "TB_User_GS",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_TaskRegister_GS_TB_TaskWork_GS_TaskWorkId",
                table: "TB_TaskRegister_GS");

            migrationBuilder.DropForeignKey(
                name: "FK_TB_TaskRegister_GS_TB_User_GS_UserId",
                table: "TB_TaskRegister_GS");

            migrationBuilder.DropIndex(
                name: "IX_TB_TaskRegister_GS_TaskWorkId",
                table: "TB_TaskRegister_GS");

            migrationBuilder.DropColumn(
                name: "TaskWorkId",
                table: "TB_TaskRegister_GS");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "TB_TaskRegister_GS",
                type: "NUMBER(10)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "NUMBER(10)");

            migrationBuilder.AddColumn<int>(
                name: "TaskId",
                table: "TB_TaskRegister_GS",
                type: "NUMBER(10)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TB_TaskRegister_GS_TaskId",
                table: "TB_TaskRegister_GS",
                column: "TaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_TaskRegister_GS_TB_TaskWork_GS_TaskId",
                table: "TB_TaskRegister_GS",
                column: "TaskId",
                principalTable: "TB_TaskWork_GS",
                principalColumn: "TaskWorkId");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_TaskRegister_GS_TB_User_GS_UserId",
                table: "TB_TaskRegister_GS",
                column: "UserId",
                principalTable: "TB_User_GS",
                principalColumn: "UserId");
        }
    }
}

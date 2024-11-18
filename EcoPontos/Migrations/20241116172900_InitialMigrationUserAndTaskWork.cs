using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoPontos.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigrationUserAndTaskWork : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_Reward_GS",
                columns: table => new
                {
                    RewardId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Description = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    NecessaryPoints = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Reward_GS", x => x.RewardId);
                });

            migrationBuilder.CreateTable(
                name: "TB_TaskWork_GS",
                columns: table => new
                {
                    TaskWorkId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Type = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    Description = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_TaskWork_GS", x => x.TaskWorkId);
                });

            migrationBuilder.CreateTable(
                name: "TB_User_GS",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Name = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR2(255)", maxLength: 255, nullable: false),
                    Password = table.Column<string>(type: "NVARCHAR2(255)", maxLength: 255, nullable: false),
                    RegisterDate = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    Points = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_User_GS", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "TB_TaskRegister_GS",
                columns: table => new
                {
                    RegisterId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    UserId = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    TaskId = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    TaskDateCompleted = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    Duration = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    PointsTotal = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_TaskRegister_GS", x => x.RegisterId);
                    table.ForeignKey(
                        name: "FK_TB_TaskRegister_GS_TB_TaskWork_GS_TaskId",
                        column: x => x.TaskId,
                        principalTable: "TB_TaskWork_GS",
                        principalColumn: "TaskWorkId");
                    table.ForeignKey(
                        name: "FK_TB_TaskRegister_GS_TB_User_GS_UserId",
                        column: x => x.UserId,
                        principalTable: "TB_User_GS",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_TaskRegister_GS_TaskId",
                table: "TB_TaskRegister_GS",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_TaskRegister_GS_UserId",
                table: "TB_TaskRegister_GS",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_Reward_GS");

            migrationBuilder.DropTable(
                name: "TB_TaskRegister_GS");

            migrationBuilder.DropTable(
                name: "TB_TaskWork_GS");

            migrationBuilder.DropTable(
                name: "TB_User_GS");
        }
    }
}

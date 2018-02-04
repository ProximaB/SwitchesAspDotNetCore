using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SwitchesAPI.DB.Migrations
{
    public partial class InitialMigration_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSwitch_Switches_SwitchId",
                table: "UserSwitch");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSwitch_Users_UserId",
                table: "UserSwitch");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserSwitch",
                table: "UserSwitch");

            migrationBuilder.RenameTable(
                name: "UserSwitch",
                newName: "UserSwitches");

            migrationBuilder.RenameIndex(
                name: "IX_UserSwitch_SwitchId",
                table: "UserSwitches",
                newName: "IX_UserSwitches_SwitchId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserSwitches",
                table: "UserSwitches",
                columns: new[] { "UserId", "SwitchId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserSwitches_Switches_SwitchId",
                table: "UserSwitches",
                column: "SwitchId",
                principalTable: "Switches",
                principalColumn: "SwitchId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSwitches_Users_UserId",
                table: "UserSwitches",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSwitches_Switches_SwitchId",
                table: "UserSwitches");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSwitches_Users_UserId",
                table: "UserSwitches");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserSwitches",
                table: "UserSwitches");

            migrationBuilder.RenameTable(
                name: "UserSwitches",
                newName: "UserSwitch");

            migrationBuilder.RenameIndex(
                name: "IX_UserSwitches_SwitchId",
                table: "UserSwitch",
                newName: "IX_UserSwitch_SwitchId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserSwitch",
                table: "UserSwitch",
                columns: new[] { "UserId", "SwitchId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserSwitch_Switches_SwitchId",
                table: "UserSwitch",
                column: "SwitchId",
                principalTable: "Switches",
                principalColumn: "SwitchId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSwitch_Users_UserId",
                table: "UserSwitch",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

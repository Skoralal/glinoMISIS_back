using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace glinoMISIS_back.Migrations
{
    /// <inheritdoc />
    public partial class _4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Compartments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compartments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Login = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    HashedPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentConpartmentID = table.Column<int>(type: "int", nullable: false),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Certificates = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Login);
                });

            migrationBuilder.CreateTable(
                name: "HardSkill",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Group = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeLogin = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HardSkill", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HardSkill_Employees_EmployeeLogin",
                        column: x => x.EmployeeLogin,
                        principalTable: "Employees",
                        principalColumn: "Login");
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Information = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeLogin = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notification_Employees_EmployeeLogin",
                        column: x => x.EmployeeLogin,
                        principalTable: "Employees",
                        principalColumn: "Login");
                });

            migrationBuilder.CreateTable(
                name: "PlaceOfWork",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JoinDate = table.Column<long>(type: "bigint", nullable: false),
                    LeaveDate = table.Column<long>(type: "bigint", nullable: false),
                    EmployeeLogin = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaceOfWork", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlaceOfWork_Employees_EmployeeLogin",
                        column: x => x.EmployeeLogin,
                        principalTable: "Employees",
                        principalColumn: "Login");
                });

            migrationBuilder.CreateTable(
                name: "SoftSkill",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Group = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeLogin = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoftSkill", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SoftSkill_Employees_EmployeeLogin",
                        column: x => x.EmployeeLogin,
                        principalTable: "Employees",
                        principalColumn: "Login");
                });

            migrationBuilder.CreateIndex(
                name: "IX_HardSkill_EmployeeLogin",
                table: "HardSkill",
                column: "EmployeeLogin");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_EmployeeLogin",
                table: "Notification",
                column: "EmployeeLogin");

            migrationBuilder.CreateIndex(
                name: "IX_PlaceOfWork_EmployeeLogin",
                table: "PlaceOfWork",
                column: "EmployeeLogin");

            migrationBuilder.CreateIndex(
                name: "IX_SoftSkill_EmployeeLogin",
                table: "SoftSkill",
                column: "EmployeeLogin");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Compartments");

            migrationBuilder.DropTable(
                name: "HardSkill");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "PlaceOfWork");

            migrationBuilder.DropTable(
                name: "SoftSkill");

            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}

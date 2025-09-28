using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VaccinationManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    Cpf = table.Column<string>(type: "TEXT", maxLength: 11, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.Cpf);
                });

            migrationBuilder.CreateTable(
                name: "Vaccines",
                columns: table => new
                {
                    VaccineId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    VaccineName = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vaccines", x => x.VaccineId);
                });

            migrationBuilder.CreateTable(
                name: "Vaccinations",
                columns: table => new
                {
                    VaccinationId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PersonCpf = table.Column<string>(type: "TEXT", maxLength: 11, nullable: false),
                    VaccineId = table.Column<int>(type: "INTEGER", nullable: false),
                    VaccineName = table.Column<string>(type: "TEXT", nullable: false),
                    VaccinationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Dose = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vaccinations", x => x.VaccinationId);
                    table.ForeignKey(
                        name: "FK_Vaccinations_People_PersonCpf",
                        column: x => x.PersonCpf,
                        principalTable: "People",
                        principalColumn: "Cpf",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vaccinations_Vaccines_VaccineId",
                        column: x => x.VaccineId,
                        principalTable: "Vaccines",
                        principalColumn: "VaccineId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vaccinations_PersonCpf",
                table: "Vaccinations",
                column: "PersonCpf");

            migrationBuilder.CreateIndex(
                name: "IX_Vaccinations_VaccineId",
                table: "Vaccinations",
                column: "VaccineId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vaccinations");

            migrationBuilder.DropTable(
                name: "People");

            migrationBuilder.DropTable(
                name: "Vaccines");
        }
    }
}

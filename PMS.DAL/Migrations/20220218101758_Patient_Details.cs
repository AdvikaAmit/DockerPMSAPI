using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PMS.DAL.Migrations
{
    public partial class Patient_Details : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PatientDetails",
                columns: table => new
                {
                    Patient_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ContactNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Race = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ethnicity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Emergency_Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Emergency_FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Emergency_LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Emergency_EmailId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Emergency_ContactNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Emergency_Relation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Emergency_Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Access_To_Patient_Portal = table.Column<bool>(type: "bit", nullable: false),
                    Allergy_Details = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientDetails", x => x.Patient_Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PatientDetails");
        }
    }
}

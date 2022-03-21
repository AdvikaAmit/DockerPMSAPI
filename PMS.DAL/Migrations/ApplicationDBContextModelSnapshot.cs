﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PMS.DAL;

namespace PMS.DAL.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    partial class ApplicationDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PMS.Domain.Entiites.Allergy", b =>
                {
                    b.Property<int>("Allergy_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Allerginicity")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Allergy_Clinical_Information")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Allergy_Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Allergy_Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Allergy_Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Allergy_Source")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Allergy_Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Allergy_Id");

                    b.ToTable("Allergies");
                });

            modelBuilder.Entity("PMS.Domain.Entiites.Appointment", b =>
                {
                    b.Property<int>("Appointment_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("Appointment_Date")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Appointment_Time")
                        .HasColumnType("datetime2");

                    b.Property<int?>("Nurse_Id")
                        .HasColumnType("int");

                    b.Property<int?>("Patient_Id")
                        .HasColumnType("int");

                    b.Property<int?>("Physician_Id")
                        .HasColumnType("int");

                    b.Property<string>("Reason")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Schedular_Id")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Appointment_Id");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("PMS.Domain.Entiites.Diagnosis", b =>
                {
                    b.Property<int>("Diagnosis_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Diagnosis_Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Diagnosis_Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("Diagnosis_Is_Depricated")
                        .HasColumnType("bit");

                    b.HasKey("Diagnosis_Id");

                    b.ToTable("Diagnoses");
                });

            modelBuilder.Entity("PMS.Domain.Entiites.DrugData", b =>
                {
                    b.Property<int>("Drug_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Drug_Form")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Drug_Generic_Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Drug_Manufacture_Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Drug_Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Drug_Strength")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Drug_ID");

                    b.ToTable("DrugDatas");
                });

            modelBuilder.Entity("PMS.Domain.Entiites.EmergencyContact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CountryCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmailId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("Is_Allowed")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RelationShip")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("EmergencyContact");
                });

            modelBuilder.Entity("PMS.Domain.Entiites.Log", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Controller")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Method")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Logs");
                });

            modelBuilder.Entity("PMS.Domain.Entiites.Notes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("date")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Notes");
                });

            modelBuilder.Entity("PMS.Domain.Entiites.PatientAllergies", b =>
                {
                    b.Property<int>("Patient_Allergy_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("Allergy_Id")
                        .HasColumnType("int");

                    b.Property<string>("Allergy_Type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("Is_Allergy_Fatal")
                        .HasColumnType("bit");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Patient_Allergy_Id");

                    b.ToTable("PatientAllergies");
                });

            modelBuilder.Entity("PMS.Domain.Entiites.PatientVisitDiagnosis", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Diagnosis_Id")
                        .HasColumnType("int");

                    b.Property<int?>("Visit_Id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("PatientVisitDiagnoses");
                });

            modelBuilder.Entity("PMS.Domain.Entiites.PatientVisitMedication", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Dosage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Drug_Id")
                        .HasColumnType("int");

                    b.Property<int?>("Visit_Id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("PatientVisitMedications");
                });

            modelBuilder.Entity("PMS.Domain.Entiites.PatientVisitProcedures", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Procedure_Id")
                        .HasColumnType("int");

                    b.Property<int?>("Visit_Id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("PatientVisitProcedures");
                });

            modelBuilder.Entity("PMS.Domain.Entiites.PatientVisits", b =>
                {
                    b.Property<int>("Visit_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Blood_Pressure")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Body_Temperature")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Height")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Respiration_Rate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Visit_Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Weight")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Visit_Id");

                    b.ToTable("PatientVisits");
                });

            modelBuilder.Entity("PMS.Domain.Entiites.Procedure", b =>
                {
                    b.Property<int>("Procedure_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Procedure_Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Procedure_Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("Procedure_Is_Depricated")
                        .HasColumnType("bit");

                    b.HasKey("Procedure_ID");

                    b.ToTable("Procedures");
                });

            modelBuilder.Entity("PMS.Domain.Entiites.Registration", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ContactNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DOB")
                        .HasColumnType("datetime2");

                    b.Property<string>("EmailId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsLoggedin")
                        .HasColumnType("bit");

                    b.Property<bool?>("Is_SetDefault")
                        .HasColumnType("bit");

                    b.Property<string>("Known_Languages")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("LoginAttempts")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Registration");
                });

            modelBuilder.Entity("PMS.Domain.Entiites.Roles", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("PMS.Domain.Entiites.UserAddress", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CountryCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Line1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Line2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("UserAddresses");
                });

            modelBuilder.Entity("PMS.Domain.Entiites.UserRoles", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("RoleId")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}

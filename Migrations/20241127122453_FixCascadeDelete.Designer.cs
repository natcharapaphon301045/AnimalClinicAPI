﻿// <auto-generated />
using System;
using AnimalClinicAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AnimalClinicAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241127122453_FixCascadeDelete")]
    partial class FixCascadeDelete
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AnimalClinicAPI.Models.Appointment", b =>
                {
                    b.Property<int>("Appointment_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Appointment_ID"));

                    b.Property<DateTime>("AppointmentDate")
                        .HasColumnType("datetime2");

                    b.Property<TimeSpan>("AppointmentTime")
                        .HasColumnType("time");

                    b.Property<int>("Customer_ID")
                        .HasColumnType("int");

                    b.Property<int>("PetOwnerCustomer_ID")
                        .HasColumnType("int");

                    b.Property<int>("Pet_ID")
                        .HasColumnType("int");

                    b.Property<string>("StatusAppointment")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Appointment_ID");

                    b.HasIndex("PetOwnerCustomer_ID");

                    b.HasIndex("Pet_ID");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("AnimalClinicAPI.Models.MedicalRecord", b =>
                {
                    b.Property<int>("Record_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Record_ID"));

                    b.Property<DateTime>("Medical_Date")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Medicineget")
                        .HasColumnType("bit");

                    b.Property<int>("Pet_ID")
                        .HasColumnType("int");

                    b.Property<decimal>("Pet_Weight")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("TreatmentDetail")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("TreatmentType")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Record_ID");

                    b.HasIndex("Pet_ID");

                    b.ToTable("MedicalRecords");
                });

            modelBuilder.Entity("AnimalClinicAPI.Models.Medicine", b =>
                {
                    b.Property<int>("Medicine_amount")
                        .HasColumnType("int");

                    b.Property<string>("Medicine_name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Record_ID")
                        .HasColumnType("int");

                    b.HasIndex("Record_ID");

                    b.ToTable("Medicines");
                });

            modelBuilder.Entity("AnimalClinicAPI.Models.Pet", b =>
                {
                    b.Property<int>("Pet_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Pet_ID"));

                    b.Property<int>("Customer_ID")
                        .HasColumnType("int");

                    b.Property<int>("PetOwnerCustomer_ID")
                        .HasColumnType("int");

                    b.Property<int>("Pet_Age")
                        .HasColumnType("int");

                    b.Property<string>("Pet_Breed")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Pet_Color")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("Pet_Gender")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("Pet_Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Pet_ID");

                    b.HasIndex("PetOwnerCustomer_ID");

                    b.ToTable("Pets");
                });

            modelBuilder.Entity("AnimalClinicAPI.Models.PetOwner", b =>
                {
                    b.Property<int>("Customer_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Customer_ID"));

                    b.Property<string>("Customer_firstname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Customer_lastname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Phone_number")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("Customer_ID");

                    b.ToTable("PetOwners");
                });

            modelBuilder.Entity("AnimalClinicAPI.Models.Appointment", b =>
                {
                    b.HasOne("AnimalClinicAPI.Models.PetOwner", "PetOwner")
                        .WithMany()
                        .HasForeignKey("PetOwnerCustomer_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AnimalClinicAPI.Models.Pet", "Pet")
                        .WithMany()
                        .HasForeignKey("Pet_ID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Pet");

                    b.Navigation("PetOwner");
                });

            modelBuilder.Entity("AnimalClinicAPI.Models.MedicalRecord", b =>
                {
                    b.HasOne("AnimalClinicAPI.Models.Pet", "Pet")
                        .WithMany()
                        .HasForeignKey("Pet_ID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Pet");
                });

            modelBuilder.Entity("AnimalClinicAPI.Models.Medicine", b =>
                {
                    b.HasOne("AnimalClinicAPI.Models.MedicalRecord", "MedicalRecord")
                        .WithMany()
                        .HasForeignKey("Record_ID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("MedicalRecord");
                });

            modelBuilder.Entity("AnimalClinicAPI.Models.Pet", b =>
                {
                    b.HasOne("AnimalClinicAPI.Models.PetOwner", "PetOwner")
                        .WithMany()
                        .HasForeignKey("PetOwnerCustomer_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PetOwner");
                });
#pragma warning restore 612, 618
        }
    }
}

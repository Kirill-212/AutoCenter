﻿// <auto-generated />
using System;
using MicroServiceApp.ServiceRepository.ContextDB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MicroServiceApp.ServiceRepository.Migrations
{
    [DbContext(typeof(ContextDb))]
    partial class ContextDbModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("MicroServiceApp.InfrastructureLayer.Models.ActionCar", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("SharePercentage")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("ActionsCars");
                });

            modelBuilder.Entity("MicroServiceApp.InfrastructureLayer.Models.Car", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int?>("ActionCarId")
                        .HasColumnType("int");

                    b.Property<long>("CarMileage")
                        .HasColumnType("bigint");

                    b.Property<decimal>("Cost")
                        .HasColumnType("money");

                    b.Property<DateTime>("DateOfRealeseCar")
                        .HasColumnType("datetime2");

                    b.Property<string>("NameCarEquipment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VIN")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("ActionCarId");

                    b.HasIndex("VIN")
                        .IsUnique()
                        .HasFilter("[VIN] IS NOT NULL");

                    b.ToTable("Cars");
                });

            modelBuilder.Entity("MicroServiceApp.InfrastructureLayer.Models.ClientCar", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("CarId")
                        .HasColumnType("int");

                    b.Property<string>("RegisterNumber")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CarId")
                        .IsUnique();

                    b.HasIndex("RegisterNumber")
                        .IsUnique()
                        .HasFilter("[RegisterNumber] IS NOT NULL");

                    b.HasIndex("UserId");

                    b.ToTable("ClientsCars");
                });

            modelBuilder.Entity("MicroServiceApp.InfrastructureLayer.Models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("StartWorkDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("MicroServiceApp.InfrastructureLayer.Models.Img", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("NewId")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("NewId");

                    b.ToTable("Imgs");
                });

            modelBuilder.Entity("MicroServiceApp.InfrastructureLayer.Models.New", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("News");
                });

            modelBuilder.Entity("MicroServiceApp.InfrastructureLayer.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("CarId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateOfBuyCar")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("TotalCost")
                        .HasColumnType("money");

                    b.HasKey("Id");

                    b.HasIndex("CarId")
                        .IsUnique();

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("MicroServiceApp.InfrastructureLayer.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("RoleName")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleName")
                        .IsUnique()
                        .HasFilter("[RoleName] IS NOT NULL");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("MicroServiceApp.InfrastructureLayer.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("DBay")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasFilter("[Email] IS NOT NULL");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MicroServiceApp.InfrastructureLayer.Models.Car", b =>
                {
                    b.HasOne("MicroServiceApp.InfrastructureLayer.Models.ActionCar", "ActionCar")
                        .WithMany("Cars")
                        .HasForeignKey("ActionCarId");

                    b.Navigation("ActionCar");
                });

            modelBuilder.Entity("MicroServiceApp.InfrastructureLayer.Models.ClientCar", b =>
                {
                    b.HasOne("MicroServiceApp.InfrastructureLayer.Models.Car", "Car")
                        .WithOne("ClientCar")
                        .HasForeignKey("MicroServiceApp.InfrastructureLayer.Models.ClientCar", "CarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MicroServiceApp.InfrastructureLayer.Models.User", "User")
                        .WithMany("ClientsCars")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Car");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MicroServiceApp.InfrastructureLayer.Models.Employee", b =>
                {
                    b.HasOne("MicroServiceApp.InfrastructureLayer.Models.User", "User")
                        .WithOne("Employee")
                        .HasForeignKey("MicroServiceApp.InfrastructureLayer.Models.Employee", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("MicroServiceApp.InfrastructureLayer.Models.Img", b =>
                {
                    b.HasOne("MicroServiceApp.InfrastructureLayer.Models.New", "New")
                        .WithMany("Imgs")
                        .HasForeignKey("NewId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("New");
                });

            modelBuilder.Entity("MicroServiceApp.InfrastructureLayer.Models.New", b =>
                {
                    b.HasOne("MicroServiceApp.InfrastructureLayer.Models.Employee", "Employee")
                        .WithMany("News")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("MicroServiceApp.InfrastructureLayer.Models.Order", b =>
                {
                    b.HasOne("MicroServiceApp.InfrastructureLayer.Models.Car", "Car")
                        .WithOne("Orders")
                        .HasForeignKey("MicroServiceApp.InfrastructureLayer.Models.Order", "CarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Car");
                });

            modelBuilder.Entity("MicroServiceApp.InfrastructureLayer.Models.User", b =>
                {
                    b.HasOne("MicroServiceApp.InfrastructureLayer.Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("MicroServiceApp.InfrastructureLayer.Models.ActionCar", b =>
                {
                    b.Navigation("Cars");
                });

            modelBuilder.Entity("MicroServiceApp.InfrastructureLayer.Models.Car", b =>
                {
                    b.Navigation("ClientCar");

                    b.Navigation("Orders");
                });

            modelBuilder.Entity("MicroServiceApp.InfrastructureLayer.Models.Employee", b =>
                {
                    b.Navigation("News");
                });

            modelBuilder.Entity("MicroServiceApp.InfrastructureLayer.Models.New", b =>
                {
                    b.Navigation("Imgs");
                });

            modelBuilder.Entity("MicroServiceApp.InfrastructureLayer.Models.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("MicroServiceApp.InfrastructureLayer.Models.User", b =>
                {
                    b.Navigation("ClientsCars");

                    b.Navigation("Employee");
                });
#pragma warning restore 612, 618
        }
    }
}

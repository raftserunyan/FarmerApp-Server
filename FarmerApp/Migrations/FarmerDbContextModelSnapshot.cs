﻿// <auto-generated />
using System;
using FarmerApp.DataAccess.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FarmerApp.Migrations
{
    [DbContext(typeof(FarmerDbContext))]
    partial class FarmerDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FarmerApp.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AccountNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HVHH")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("FarmerApp.Models.Expense", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("ExpenseAmount")
                        .HasColumnType("int");

                    b.Property<string>("ExpenseName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TargetId")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TargetId");

                    b.HasIndex("UserId");

                    b.ToTable("Expenses");
                });

            modelBuilder.Entity("FarmerApp.Models.Investment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("InvestorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("InvestorId");

                    b.ToTable("Investments");
                });

            modelBuilder.Entity("FarmerApp.Models.Investor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Investors");
                });

            modelBuilder.Entity("FarmerApp.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PriceKG")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("FarmerApp.Models.Sale", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("Paid")
                        .HasColumnType("int");

                    b.Property<int>("PriceKG")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.Property<double>("Weight")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("ProductId");

                    b.HasIndex("UserId");

                    b.ToTable("Sales");
                });

            modelBuilder.Entity("FarmerApp.Models.Target", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Targets");
                });

            modelBuilder.Entity("FarmerApp.Models.Treatment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("DrugName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DrugWeight")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Treatments");
                });

            modelBuilder.Entity("FarmerApp.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ProductTreatment", b =>
                {
                    b.Property<int>("ProductsId")
                        .HasColumnType("int");

                    b.Property<int>("TreatmentsId")
                        .HasColumnType("int");

                    b.HasKey("ProductsId", "TreatmentsId");

                    b.HasIndex("TreatmentsId");

                    b.ToTable("ProductTreatment");
                });

            modelBuilder.Entity("FarmerApp.Models.Customer", b =>
                {
                    b.HasOne("FarmerApp.Models.User", "User")
                        .WithMany("Customers")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FarmerApp.Models.Expense", b =>
                {
                    b.HasOne("FarmerApp.Models.Target", "Target")
                        .WithMany("Expenses")
                        .HasForeignKey("TargetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FarmerApp.Models.User", "User")
                        .WithMany("Expenses")
                        .HasForeignKey("UserId");

                    b.Navigation("Target");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FarmerApp.Models.Investment", b =>
                {
                    b.HasOne("FarmerApp.Models.Investor", "Investor")
                        .WithMany("Investments")
                        .HasForeignKey("InvestorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Investor");
                });

            modelBuilder.Entity("FarmerApp.Models.Investor", b =>
                {
                    b.HasOne("FarmerApp.Models.User", "User")
                        .WithMany("Investors")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FarmerApp.Models.Product", b =>
                {
                    b.HasOne("FarmerApp.Models.User", "User")
                        .WithMany("Products")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FarmerApp.Models.Sale", b =>
                {
                    b.HasOne("FarmerApp.Models.Customer", "CurrentCustomer")
                        .WithMany("Sales")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FarmerApp.Models.Product", "CurrentProduct")
                        .WithMany("Sales")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FarmerApp.Models.User", "User")
                        .WithMany("Sales")
                        .HasForeignKey("UserId");

                    b.Navigation("CurrentCustomer");

                    b.Navigation("CurrentProduct");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FarmerApp.Models.Target", b =>
                {
                    b.HasOne("FarmerApp.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FarmerApp.Models.Treatment", b =>
                {
                    b.HasOne("FarmerApp.Models.User", "User")
                        .WithMany("Treatments")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ProductTreatment", b =>
                {
                    b.HasOne("FarmerApp.Models.Product", null)
                        .WithMany()
                        .HasForeignKey("ProductsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FarmerApp.Models.Treatment", null)
                        .WithMany()
                        .HasForeignKey("TreatmentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FarmerApp.Models.Customer", b =>
                {
                    b.Navigation("Sales");
                });

            modelBuilder.Entity("FarmerApp.Models.Investor", b =>
                {
                    b.Navigation("Investments");
                });

            modelBuilder.Entity("FarmerApp.Models.Product", b =>
                {
                    b.Navigation("Sales");
                });

            modelBuilder.Entity("FarmerApp.Models.Target", b =>
                {
                    b.Navigation("Expenses");
                });

            modelBuilder.Entity("FarmerApp.Models.User", b =>
                {
                    b.Navigation("Customers");

                    b.Navigation("Expenses");

                    b.Navigation("Investors");

                    b.Navigation("Products");

                    b.Navigation("Sales");

                    b.Navigation("Treatments");
                });
#pragma warning restore 612, 618
        }
    }
}

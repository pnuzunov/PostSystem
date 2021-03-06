﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PostSystem.Data;

namespace PostSystem.Data.Migrations
{
    [DbContext(typeof(PostSystemDbContext))]
    [Migration("20200522205145_Initial_Migration")]
    partial class Initial_Migration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PostSystem.Models.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City_Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("City_Post_Code")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created_On")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Updated_On")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("PostSystem.Models.Delivery", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created_On")
                        .HasColumnType("datetime2");

                    b.Property<int?>("Delivery_MailId")
                        .HasColumnType("int");

                    b.Property<string>("Details")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Express_Delivery")
                        .HasColumnType("bit");

                    b.Property<int>("From_Office_Id")
                        .HasColumnType("int");

                    b.Property<int>("Mail_Id")
                        .HasColumnType("int");

                    b.Property<double>("Tax")
                        .HasColumnType("float");

                    b.Property<int>("To_Office_Id")
                        .HasColumnType("int");

                    b.Property<DateTime>("Updated_On")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("Delivery_MailId");

                    b.HasIndex("From_Office_Id");

                    b.HasIndex("To_Office_Id");

                    b.ToTable("Deliveries");
                });

            modelBuilder.Entity("PostSystem.Models.Mail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created_On")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Height")
                        .HasColumnType("float");

                    b.Property<double>("Length")
                        .HasColumnType("float");

                    b.Property<DateTime>("Updated_On")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Weight")
                        .HasColumnType("decimal(18,2)");

                    b.Property<double>("Width")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Mails");
                });

            modelBuilder.Entity("PostSystem.Models.PostOffice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("City_Id")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created_On")
                        .HasColumnType("datetime2");

                    b.Property<short>("Desk_Count")
                        .HasColumnType("smallint");

                    b.Property<DateTime>("Updated_On")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("City_Id");

                    b.ToTable("PostOffices");
                });

            modelBuilder.Entity("PostSystem.Models.Delivery", b =>
                {
                    b.HasOne("PostSystem.Models.Mail", "Delivery_Mail")
                        .WithMany()
                        .HasForeignKey("Delivery_MailId");

                    b.HasOne("PostSystem.Models.PostOffice", "From_Delivery_Office")
                        .WithMany("From_Deliveries")
                        .HasForeignKey("From_Office_Id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("PostSystem.Models.PostOffice", "To_Delivery_Office")
                        .WithMany("To_Deliveries")
                        .HasForeignKey("To_Office_Id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("PostSystem.Models.PostOffice", b =>
                {
                    b.HasOne("PostSystem.Models.City", "Office_City")
                        .WithMany("PostOffices")
                        .HasForeignKey("City_Id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}

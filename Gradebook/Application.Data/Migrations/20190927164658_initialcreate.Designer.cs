﻿// <auto-generated />
using AppCore.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AppCore.Data.Migrations
{
    [DbContext(typeof(BusinessDBContext))]
    [Migration("20190927164658_initialcreate")]
    partial class initialcreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ACM.BL.Address", b =>
                {
                    b.Property<int>("AddressId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AddressType");

                    b.Property<string>("City");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<int>("EntityState");

                    b.Property<bool>("HasChanges");

                    b.Property<bool>("IsNew");

                    b.Property<string>("PostalCode");

                    b.Property<string>("State");

                    b.Property<string>("StreetLine1");

                    b.Property<string>("StreetLine2");

                    b.HasKey("AddressId");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("ACM.BL.Restaurant", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AddressId");

                    b.Property<int>("Cuisine");

                    b.Property<int>("EntityState");

                    b.Property<bool>("HasChanges");

                    b.Property<bool>("IsNew");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80);

                    b.HasKey("ID");

                    b.HasIndex("AddressId");

                    b.ToTable("Restaurants");
                });

            modelBuilder.Entity("ACM.BL.Restaurant", b =>
                {
                    b.HasOne("ACM.BL.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}

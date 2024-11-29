﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ReadersRent.Context;

#nullable disable

namespace ReadersRent.Migrations
{
    [DbContext(typeof(DBCon))]
    partial class DBConModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ReadersRent.Model.Reader", b =>
                {
                    b.Property<int>("Id_Reader")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id_Reader"));

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id_Reader");

                    b.ToTable("Reader");
                });

            modelBuilder.Entity("ReadersRent.Model.Rent", b =>
                {
                    b.Property<int>("ID_Rent")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID_Rent"));

                    b.Property<DateTime?>("Date_End")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Date_Start")
                        .HasColumnType("datetime2");

                    b.Property<int>("ID_Book")
                        .HasColumnType("int");

                    b.Property<int>("ID_Reader")
                        .HasColumnType("int");

                    b.Property<int>("Srok")
                        .HasColumnType("int");

                    b.HasKey("ID_Rent");

                    b.ToTable("Rent");
                });
#pragma warning restore 612, 618
        }
    }
}

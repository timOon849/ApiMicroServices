﻿// <auto-generated />
using System;
using ImageApi.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ImageApi.Migrations
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

            modelBuilder.Entity("ImageApi.Model.Image", b =>
                {
                    b.Property<int>("Id_image")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id_image"));

                    b.Property<string>("ContentType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Create_date")
                        .HasColumnType("datetime2");

                    b.Property<byte[]>("File")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.HasKey("Id_image");

                    b.ToTable("Image");
                });
#pragma warning restore 612, 618
        }
    }
}

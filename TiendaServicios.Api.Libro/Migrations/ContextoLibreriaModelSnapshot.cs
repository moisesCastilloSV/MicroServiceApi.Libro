﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TiendaServicios.Api.Libro.CTX;

#nullable disable

namespace TiendaServicios.Api.Libro.Migrations
{
    [DbContext(typeof(ContextoLibreria))]
    partial class ContextoLibreriaModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("TiendaServicios.Api.Libro.Model.LibreriaMaterial", b =>
                {
                    b.Property<int>("LibreriaMaterialId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LibreriaMaterialId"), 1L, 1);

                    b.Property<Guid?>("AutorLibroId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("FechaPublicacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LibreriaMaterialId");

                    b.ToTable("libreriaMaterial");
                });
#pragma warning restore 612, 618
        }
    }
}
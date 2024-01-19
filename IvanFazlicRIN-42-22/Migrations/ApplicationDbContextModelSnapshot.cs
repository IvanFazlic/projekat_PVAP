﻿// <auto-generated />
using IvanFazlicRIN_42_22;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace IvanFazlicRIN_42_22.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Artikal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BojaId")
                        .HasColumnType("int");

                    b.Property<decimal>("Cena")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BojaId");

                    b.ToTable("Artikli");
                });

            modelBuilder.Entity("IvanFazlicRIN_42_22.Modals.ArtikalKategorija", b =>
                {
                    b.Property<int>("ArtikalId")
                        .HasColumnType("int");

                    b.Property<int>("KategorijaId")
                        .HasColumnType("int");

                    b.HasKey("ArtikalId", "KategorijaId");

                    b.HasIndex("KategorijaId");

                    b.ToTable("ArtikalKategorije");
                });

            modelBuilder.Entity("IvanFazlicRIN_42_22.Modals.Boja", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Boje");
                });

            modelBuilder.Entity("IvanFazlicRIN_42_22.Modals.Kategorija", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Kategorije");
                });

            modelBuilder.Entity("IvanFazlicRIN_42_22.Modals.Komentar", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ArtikalId")
                        .HasColumnType("int");

                    b.Property<string>("Tekst")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ArtikalId");

                    b.ToTable("Komentari");
                });

            modelBuilder.Entity("Artikal", b =>
                {
                    b.HasOne("IvanFazlicRIN_42_22.Modals.Boja", "Boja")
                        .WithMany("Artikli")
                        .HasForeignKey("BojaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Boja");
                });

            modelBuilder.Entity("IvanFazlicRIN_42_22.Modals.ArtikalKategorija", b =>
                {
                    b.HasOne("Artikal", "Artikal")
                        .WithMany("ArtikalKategorije")
                        .HasForeignKey("ArtikalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IvanFazlicRIN_42_22.Modals.Kategorija", "Kategorija")
                        .WithMany("ArtikalKategorije")
                        .HasForeignKey("KategorijaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Artikal");

                    b.Navigation("Kategorija");
                });

            modelBuilder.Entity("IvanFazlicRIN_42_22.Modals.Komentar", b =>
                {
                    b.HasOne("Artikal", "Artikal")
                        .WithMany("Komentari")
                        .HasForeignKey("ArtikalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Artikal");
                });

            modelBuilder.Entity("Artikal", b =>
                {
                    b.Navigation("ArtikalKategorije");

                    b.Navigation("Komentari");
                });

            modelBuilder.Entity("IvanFazlicRIN_42_22.Modals.Boja", b =>
                {
                    b.Navigation("Artikli");
                });

            modelBuilder.Entity("IvanFazlicRIN_42_22.Modals.Kategorija", b =>
                {
                    b.Navigation("ArtikalKategorije");
                });
#pragma warning restore 612, 618
        }
    }
}

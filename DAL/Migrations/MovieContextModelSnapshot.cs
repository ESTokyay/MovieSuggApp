﻿// <auto-generated />
using System;
using DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DAL.Migrations
{
    [DbContext(typeof(MovieContext))]
    partial class MovieContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DATA.Entity.Film", b =>
                {
                    b.Property<int>("EntityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MovieEntityId")
                        .HasColumnType("int");

                    b.Property<bool>("adult")
                        .HasColumnType("bit");

                    b.Property<string>("backdrop_path")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("id")
                        .HasColumnType("int");

                    b.Property<string>("original_language")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("original_title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("overview")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("popularity")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("poster_path")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("release_date")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("video")
                        .HasColumnType("bit");

                    b.Property<decimal>("vote_average")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("vote_count")
                        .HasColumnType("int");

                    b.HasKey("EntityId");

                    b.HasIndex("MovieEntityId");

                    b.ToTable("Film");
                });

            modelBuilder.Entity("DATA.Entity.MovieEntity", b =>
                {
                    b.Property<int>("EntityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("page")
                        .HasColumnType("int");

                    b.Property<int>("total_pages")
                        .HasColumnType("int");

                    b.Property<int>("total_results")
                        .HasColumnType("int");

                    b.HasKey("EntityId");

                    b.ToTable("MovieEntity");
                });

            modelBuilder.Entity("DATA.Entity.Notlar", b =>
                {
                    b.Property<int>("EntityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Icerik")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("filmEntityId")
                        .HasColumnType("int");

                    b.Property<int?>("kullaniciEntityId")
                        .HasColumnType("int");

                    b.HasKey("EntityId");

                    b.HasIndex("filmEntityId");

                    b.HasIndex("kullaniciEntityId");

                    b.ToTable("Notlar");
                });

            modelBuilder.Entity("DATA.Entity.Puanlar", b =>
                {
                    b.Property<int>("EntityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Puan")
                        .HasColumnType("int");

                    b.Property<int?>("filmEntityId")
                        .HasColumnType("int");

                    b.Property<int?>("kullaniciEntityId")
                        .HasColumnType("int");

                    b.HasKey("EntityId");

                    b.HasIndex("filmEntityId");

                    b.HasIndex("kullaniciEntityId");

                    b.ToTable("Puanlar");
                });

            modelBuilder.Entity("DATA.Entity.Users", b =>
                {
                    b.Property<int>("EntityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EntityId");

                    b.ToTable("Kullanicilar");
                });

            modelBuilder.Entity("DATA.Entity.Film", b =>
                {
                    b.HasOne("DATA.Entity.MovieEntity", "MovieEntity")
                        .WithMany("results")
                        .HasForeignKey("MovieEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MovieEntity");
                });

            modelBuilder.Entity("DATA.Entity.Notlar", b =>
                {
                    b.HasOne("DATA.Entity.Film", "film")
                        .WithMany("notlar_list")
                        .HasForeignKey("filmEntityId");

                    b.HasOne("DATA.Entity.Users", "kullanici")
                        .WithMany("notlar")
                        .HasForeignKey("kullaniciEntityId");

                    b.Navigation("film");

                    b.Navigation("kullanici");
                });

            modelBuilder.Entity("DATA.Entity.Puanlar", b =>
                {
                    b.HasOne("DATA.Entity.Film", "film")
                        .WithMany("puanlar_list")
                        .HasForeignKey("filmEntityId");

                    b.HasOne("DATA.Entity.Users", "kullanici")
                        .WithMany("puanlar")
                        .HasForeignKey("kullaniciEntityId");

                    b.Navigation("film");

                    b.Navigation("kullanici");
                });

            modelBuilder.Entity("DATA.Entity.Film", b =>
                {
                    b.Navigation("notlar_list");

                    b.Navigation("puanlar_list");
                });

            modelBuilder.Entity("DATA.Entity.MovieEntity", b =>
                {
                    b.Navigation("results");
                });

            modelBuilder.Entity("DATA.Entity.Users", b =>
                {
                    b.Navigation("notlar");

                    b.Navigation("puanlar");
                });
#pragma warning restore 612, 618
        }
    }
}

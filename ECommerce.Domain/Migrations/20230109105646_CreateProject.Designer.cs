﻿// <auto-generated />
using System;
using ECommerce.Domain.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ECommerce.Domain.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20230109105646_CreateProject")]
    partial class CreateProject
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ECommerce.Domain.Entities.Security.Authorization", b =>
                {
                    b.Property<int>("Code")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("Deleted")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("DeletionZamani")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Descripton")
                        .IsRequired()
                        .HasMaxLength(511)
                        .HasColumnType("character varying(511)");

                    b.Property<Guid>("Guid")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("ModificationTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(127)
                        .HasColumnType("character varying(127)");

                    b.HasKey("Code");

                    b.HasIndex("Guid")
                        .IsUnique();

                    b.HasIndex("Name", "Deleted")
                        .IsUnique();

                    b.ToTable("Authorization");
                });

            modelBuilder.Entity("ECommerce.Domain.Entities.Security.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("Deleted")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("DeletionZamani")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(511)
                        .HasColumnType("character varying(511)");

                    b.Property<Guid>("Guid")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("ModificationTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(127)
                        .HasColumnType("character varying(127)");

                    b.HasKey("Id");

                    b.HasIndex("Guid")
                        .IsUnique();

                    b.HasIndex("Name", "Deleted")
                        .IsUnique();

                    b.ToTable("Role");
                });

            modelBuilder.Entity("ECommerce.Domain.Entities.Security.RoleAuthorization", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AuthorizationId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("Deleted")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("DeletionZamani")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("Guid")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("ModificationTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AuthorizationId");

                    b.HasIndex("Guid")
                        .IsUnique();

                    b.HasIndex("RoleId", "AuthorizationId", "Deleted")
                        .IsUnique();

                    b.ToTable("RoleAuthorization");
                });

            modelBuilder.Entity("ECommerce.Domain.Entities.Security.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("Deleted")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("DeletionZamani")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("EMail")
                        .IsRequired()
                        .HasMaxLength(127)
                        .HasColumnType("character varying(127)");

                    b.Property<Guid>("Guid")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("ModificationTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(63)
                        .HasColumnType("character varying(63)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(63)
                        .HasColumnType("character varying(63)");

                    b.Property<string>("PasswordAgain")
                        .IsRequired()
                        .HasMaxLength(63)
                        .HasColumnType("character varying(63)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(63)
                        .HasColumnType("character varying(63)");

                    b.Property<string>("Telephone")
                        .IsRequired()
                        .HasMaxLength(31)
                        .HasColumnType("character varying(31)");

                    b.Property<string>("TuzlamaDegeri")
                        .IsRequired()
                        .HasMaxLength(63)
                        .HasColumnType("character varying(63)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(127)
                        .HasColumnType("character varying(127)");

                    b.HasKey("Id");

                    b.HasIndex("Guid")
                        .IsUnique();

                    b.HasIndex("UserName", "Deleted")
                        .IsUnique();

                    b.ToTable("User");
                });

            modelBuilder.Entity("ECommerce.Domain.Entities.Security.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("Deleted")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("DeletionZamani")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("Guid")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("ModificationTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("Guid")
                        .IsUnique();

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId", "RoleId", "Deleted")
                        .IsUnique();

                    b.ToTable("UserRole");
                });

            modelBuilder.Entity("ECommerce.Domain.Entities.Security.RoleAuthorization", b =>
                {
                    b.HasOne("ECommerce.Domain.Entities.Security.Authorization", "Authorization")
                        .WithMany()
                        .HasForeignKey("AuthorizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ECommerce.Domain.Entities.Security.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Authorization");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("ECommerce.Domain.Entities.Security.UserRole", b =>
                {
                    b.HasOne("ECommerce.Domain.Entities.Security.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ECommerce.Domain.Entities.Security.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}

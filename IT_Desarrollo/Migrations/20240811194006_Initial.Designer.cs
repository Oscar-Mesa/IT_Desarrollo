﻿// <auto-generated />
using System;
using GusticosWebAPI;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace IT_Desarrollo_Back.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240811194006_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.32")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("IT_Desarrollo_Back.Entidades.Pregunta", b =>
                {
                    b.Property<int>("pkid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("pkid"));

                    b.Property<string>("descripcion")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("pkid");

                    b.ToTable("tbl_preguntas");
                });

            modelBuilder.Entity("IT_Desarrollo_Back.Entidades.Respuesta", b =>
                {
                    b.Property<int>("pkid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("pkid"));

                    b.Property<int>("PreguntaId")
                        .HasColumnType("integer");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("integer");

                    b.Property<string>("pregunta")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("respuesta")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.HasKey("pkid");

                    b.HasIndex("PreguntaId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("tbl_respuestas");
                });

            modelBuilder.Entity("IT_Desarrollo_Back.Entidades.Rol", b =>
                {
                    b.Property<int>("pkid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("pkid"));

                    b.Property<string>("descripcion")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("pkid");

                    b.ToTable("tbl_roles");
                });

            modelBuilder.Entity("IT_Desarrollo_Back.Entidades.Usuario", b =>
                {
                    b.Property<int>("pkid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("pkid"));

                    b.Property<int>("RolId")
                        .HasColumnType("integer");

                    b.Property<string>("apellido")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("codigo_pais")
                        .HasColumnType("integer");

                    b.Property<string>("contrasena")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<byte[]>("img")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<string>("nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("telefono")
                        .HasColumnType("integer");

                    b.HasKey("pkid");

                    b.HasIndex("RolId");

                    b.ToTable("tbl_usuarios");
                });

            modelBuilder.Entity("IT_Desarrollo_Back.Entidades.Respuesta", b =>
                {
                    b.HasOne("IT_Desarrollo_Back.Entidades.Pregunta", "Pregunta")
                        .WithMany()
                        .HasForeignKey("PreguntaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IT_Desarrollo_Back.Entidades.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pregunta");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("IT_Desarrollo_Back.Entidades.Usuario", b =>
                {
                    b.HasOne("IT_Desarrollo_Back.Entidades.Rol", "Rol")
                        .WithMany()
                        .HasForeignKey("RolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Rol");
                });
#pragma warning restore 612, 618
        }
    }
}

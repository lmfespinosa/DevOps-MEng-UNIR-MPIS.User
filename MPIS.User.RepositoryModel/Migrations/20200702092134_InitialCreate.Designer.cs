﻿// <auto-generated />
using System;
using MPIS.User.RepositoryModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MPIS.User.RepositoryModel.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20200702092134_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MPIS.User.DomainModel.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<DateTime>("Created");

                    b.Property<DateTime?>("Deleted");

                    b.Property<string>("Email");

                    b.Property<string>("Name");

                    b.Property<string>("Office");

                    b.Property<string>("Password");

                    b.Property<string>("Phone");

                    b.Property<string>("Surname");

                    b.Property<DateTime?>("Updated");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.ToTable("Users","Core");
                });
#pragma warning restore 612, 618
        }
    }
}

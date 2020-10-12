﻿// <auto-generated />
using Advantage.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace Advantage.API.Migrations
{
    [DbContext(typeof(ApiContext))]
    partial class ApiContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Advantage.API.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("Name");

                    b.Property<string>("State");

                    b.HasKey("Id");

                    b.ToTable("customers");
                });

            modelBuilder.Entity("Advantage.API.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("Completed");

                    b.Property<decimal>("OrderTotal");

                    b.Property<DateTime>("Placed");

                    b.Property<int?>("customerId");

                    b.HasKey("Id");

                    b.HasIndex("customerId");

                    b.ToTable("orders");
                });

            modelBuilder.Entity("Advantage.API.Models.Server", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<bool>("isOnline");

                    b.HasKey("Id");

                    b.ToTable("Servers");
                });

            modelBuilder.Entity("Advantage.API.Models.Order", b =>
                {
                    b.HasOne("Advantage.API.Models.Customer", "customer")
                        .WithMany()
                        .HasForeignKey("customerId");
                });
#pragma warning restore 612, 618
        }
    }
}

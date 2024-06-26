﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using OnlineShop.Order.Api.Data;

#nullable disable

namespace OnlineShop.Order.Api.Migrations
{
    [DbContext(typeof(OrderDbContext))]
    partial class OrderDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("OnlineShop.Order.Api.Data.Entities.OrderEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Amount")
                        .HasPrecision(18, 2)
                        .HasColumnType("numeric(18,2)");

                    b.Property<int>("CustomerId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("OrderedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Orders");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Amount = 50.4m,
                            CustomerId = 1,
                            OrderedAt = new DateTime(2024, 3, 21, 4, 11, 12, 530, DateTimeKind.Utc).AddTicks(2959)
                        },
                        new
                        {
                            Id = 2,
                            Amount = 25.1m,
                            CustomerId = 1,
                            OrderedAt = new DateTime(2024, 3, 19, 4, 11, 12, 530, DateTimeKind.Utc).AddTicks(2968)
                        },
                        new
                        {
                            Id = 3,
                            Amount = 10.2m,
                            CustomerId = 2,
                            OrderedAt = new DateTime(2024, 3, 21, 4, 11, 12, 530, DateTimeKind.Utc).AddTicks(2970)
                        });
                });
#pragma warning restore 612, 618
        }
    }
}

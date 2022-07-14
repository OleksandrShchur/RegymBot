﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RegymBot.Data;

namespace RegymBot.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20220714183725_RemoveHistoryTable")]
    partial class RemoveHistoryTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("RegymBot.Data.Entities.FeedbackEntity", b =>
                {
                    b.Property<Guid>("FeedbackGuid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Feedback")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("FeedbackGuid");

                    b.ToTable("Feedbacks");
                });

            modelBuilder.Entity("RegymBot.Data.Entities.PriceEntity", b =>
                {
                    b.Property<Guid>("PriceGuid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("PriceName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PriceType")
                        .HasColumnType("int");

                    b.HasKey("PriceGuid");

                    b.ToTable("Prices");
                });
#pragma warning restore 612, 618
        }
    }
}

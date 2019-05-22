﻿// <auto-generated />
using System;
using ITISKeys.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ITISKeys.Infrastucture.Migrations
{
    [DbContext(typeof(ITISKeysDbContext))]
    partial class ITISKeysDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ITISKeys.Models.Reservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("End");

                    b.Property<int?>("ReservatorId");

                    b.Property<int?>("RoomId");

                    b.Property<DateTime>("Start");

                    b.HasKey("Id");

                    b.HasIndex("ReservatorId");

                    b.HasIndex("RoomId");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("ITISKeys.Models.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("InStock");

                    b.Property<int?>("KeeperId");

                    b.Property<int>("RoomNumber");

                    b.HasKey("Id");

                    b.HasIndex("KeeperId");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("ITISKeys.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Age");

                    b.Property<string>("Email");

                    b.Property<string>("Name");

                    b.Property<string>("Role");

                    b.Property<string>("Sex");

                    b.Property<string>("UId");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ITISKeys.Models.Reservation", b =>
                {
                    b.HasOne("ITISKeys.Models.User", "Reservator")
                        .WithMany()
                        .HasForeignKey("ReservatorId");

                    b.HasOne("ITISKeys.Models.Room", "Room")
                        .WithMany()
                        .HasForeignKey("RoomId");
                });

            modelBuilder.Entity("ITISKeys.Models.Room", b =>
                {
                    b.HasOne("ITISKeys.Models.User", "Keeper")
                        .WithMany()
                        .HasForeignKey("KeeperId");
                });
#pragma warning restore 612, 618
        }
    }
}

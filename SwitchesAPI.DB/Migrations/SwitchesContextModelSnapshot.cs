﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using SwitchesAPI.DB;
using System;

namespace SwitchesAPI.DB.Migrations
{
    [DbContext(typeof(SwitchesContext))]
    partial class SwitchesContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SwitchesAPI.DB.DbModels.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("RoomId");

                    b.Property<string>("Description");

                    b.Property<DateTime>("LastModiDateTime")
                        .HasColumnName("Last modified DateTime")
                        .HasColumnType("DateTime2");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("SwitchesAPI.DB.DbModels.Switch", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("SwitchId");

                    b.Property<string>("Description");

                    b.Property<DateTime>("LastModifieDateTime")
                        .HasColumnName("Last modified DateTime")
                        .HasColumnType("DateTime2");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("RoomId");

                    b.Property<string>("State")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoomId");

                    b.ToTable("Switches");
                });

            modelBuilder.Entity("SwitchesAPI.DB.DbModels.User", b =>
                {
                    b.Property<string>("UserName")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(20);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnName("Create Date");

                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Password");

                    b.Property<string>("PasswordSalt")
                        .HasColumnName("Password Salt");

                    b.HasKey("UserName");

                    b.HasIndex("UserName")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SwitchesAPI.Models.UserSwitch", b =>
                {
                    b.Property<string>("UserName");

                    b.Property<int>("SwitchId");

                    b.HasKey("UserName", "SwitchId");

                    b.HasIndex("SwitchId");

                    b.ToTable("UserSwitches");
                });

            modelBuilder.Entity("SwitchesAPI.DB.DbModels.Switch", b =>
                {
                    b.HasOne("SwitchesAPI.DB.DbModels.Room", "Room")
                        .WithMany("Switches")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SwitchesAPI.Models.UserSwitch", b =>
                {
                    b.HasOne("SwitchesAPI.DB.DbModels.Switch", "Switch")
                        .WithMany("UserSwitches")
                        .HasForeignKey("SwitchId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SwitchesAPI.DB.DbModels.User", "User")
                        .WithMany("UserSwitches")
                        .HasForeignKey("UserName")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}

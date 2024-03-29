﻿// <auto-generated />
using System;
using BankAccount.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BankAccount.Migrations
{
    [DbContext(typeof(MyContext))]
    partial class MyContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("BankAccount.Transactions", b =>
                {
                    b.Property<int>("TransId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("trans_id");

                    b.Property<decimal>("Amount")
                        .HasColumnName("amount");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnName("created_at");

                    b.Property<int>("UserId")
                        .HasColumnName("user_id");

                    b.HasKey("TransId");

                    b.HasIndex("UserId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("BankAccount.UserReg", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<decimal>("Balance")
                        .HasColumnName("balance");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnName("created_at");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnName("f_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnName("l_name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnName("pw");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnName("updated_at");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BankAccount.Transactions", b =>
                {
                    b.HasOne("BankAccount.UserReg", "Owner")
                        .WithMany("TransMade")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}

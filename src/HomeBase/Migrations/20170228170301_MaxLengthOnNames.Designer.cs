using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using HomeBase.Data;

namespace HomeBase.Migrations
{
    [DbContext(typeof(HomeBaseContext))]
    [Migration("20170228170301_MaxLengthOnNames")]
    partial class MaxLengthOnNames
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("HomeBase.Models.Captain", b =>
                {
                    b.Property<int>("CaptainID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<DateTime>("StartDate");

                    b.HasKey("CaptainID");

                    b.ToTable("Captain");
                });

            modelBuilder.Entity("HomeBase.Models.Enrollment", b =>
                {
                    b.Property<int>("EnrollmentID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("PlayerID");

                    b.Property<int?>("Position");

                    b.Property<int>("TeamID");

                    b.HasKey("EnrollmentID");

                    b.HasIndex("PlayerID");

                    b.HasIndex("TeamID");

                    b.ToTable("Enrollment");
                });

            modelBuilder.Entity("HomeBase.Models.Player", b =>
                {
                    b.Property<int>("PlayerID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("Email");

                    b.Property<DateTime>("EnrollmentDate");

                    b.Property<string>("Experience");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("PhoneNumber");

                    b.Property<string>("Position");

                    b.Property<int?>("TeamID");

                    b.Property<string>("TeamRequested");

                    b.HasKey("PlayerID");

                    b.HasIndex("TeamID");

                    b.ToTable("Player");
                });

            modelBuilder.Entity("HomeBase.Models.Team", b =>
                {
                    b.Property<int>("TeamID");

                    b.Property<int?>("CaptainID");

                    b.Property<string>("TeamName")
                        .HasMaxLength(50);

                    b.HasKey("TeamID");

                    b.HasIndex("CaptainID")
                        .IsUnique();

                    b.ToTable("Team");
                });

            modelBuilder.Entity("HomeBase.Models.TeamAssignment", b =>
                {
                    b.Property<int>("TeamID");

                    b.Property<int>("CaptainID");

                    b.HasKey("TeamID", "CaptainID");

                    b.HasIndex("CaptainID");

                    b.ToTable("TeamAssignment");
                });

            modelBuilder.Entity("HomeBase.Models.Enrollment", b =>
                {
                    b.HasOne("HomeBase.Models.Player", "Player")
                        .WithMany("Enrollments")
                        .HasForeignKey("PlayerID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HomeBase.Models.Team", "Team")
                        .WithMany("Enrollments")
                        .HasForeignKey("TeamID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HomeBase.Models.Player", b =>
                {
                    b.HasOne("HomeBase.Models.Team", "Team")
                        .WithMany("Players")
                        .HasForeignKey("TeamID");
                });

            modelBuilder.Entity("HomeBase.Models.Team", b =>
                {
                    b.HasOne("HomeBase.Models.Captain", "Captain")
                        .WithOne("Team")
                        .HasForeignKey("HomeBase.Models.Team", "CaptainID");
                });

            modelBuilder.Entity("HomeBase.Models.TeamAssignment", b =>
                {
                    b.HasOne("HomeBase.Models.Captain", "Captain")
                        .WithMany()
                        .HasForeignKey("CaptainID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HomeBase.Models.Team", "Team")
                        .WithMany()
                        .HasForeignKey("TeamID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}

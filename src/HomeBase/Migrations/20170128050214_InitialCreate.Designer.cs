using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using HomeBase.Data;

namespace HomeBase.Migrations
{
    [DbContext(typeof(HomeBaseContext))]
    [Migration("20170128050214_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

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

                    b.Property<string>("TeamName");

                    b.HasKey("TeamID");

                    b.ToTable("Team");
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
                        .WithMany()
                        .HasForeignKey("TeamID");
                });
        }
    }
}

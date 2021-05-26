﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RecruitmentManagementApi.Models.Entities;

namespace RecruitmentManagementApi.Migrations
{
    [DbContext(typeof(RecruitmentManagementContext))]
    partial class RecruitmentManagementContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("RecruitmentManagementApi.Models.Entities.AuthorizationKey", b =>
                {
                    b.Property<int>("AuthorizationKeyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsValid")
                        .HasColumnType("bit");

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.HasKey("AuthorizationKeyId");

                    b.ToTable("AuthorizationKeys");
                });

            modelBuilder.Entity("RecruitmentManagementApi.Models.Entities.Candidate", b =>
                {
                    b.Property<int>("CandidateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Curriculum")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("GitHub")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("CandidateId");

                    b.ToTable("Candidates");
                });

            modelBuilder.Entity("RecruitmentManagementApi.Models.Entities.Recruitment", b =>
                {
                    b.Property<int>("RecruitmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CandidateId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Note")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("RecruitmentId");

                    b.HasIndex("CandidateId")
                        .IsUnique();

                    b.ToTable("Recruitments");
                });

            modelBuilder.Entity("RecruitmentManagementApi.Models.Entities.RecruitmentUpdateHistory", b =>
                {
                    b.Property<int>("RecruitmentUpdateHistoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Note")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<int>("RecruitmentId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("RecruitmentUpdateHistoryId");

                    b.HasIndex("RecruitmentId");

                    b.ToTable("RecruitmentUpdateHistories");
                });

            modelBuilder.Entity("RecruitmentManagementApi.Models.Entities.Status", b =>
                {
                    b.Property<int>("StatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("StatusId");

                    b.ToTable("Statuses");
                });

            modelBuilder.Entity("RecruitmentManagementApi.Models.Entities.Recruitment", b =>
                {
                    b.HasOne("RecruitmentManagementApi.Models.Entities.Candidate", "Candidate")
                        .WithOne("Recruitment")
                        .HasForeignKey("RecruitmentManagementApi.Models.Entities.Recruitment", "CandidateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Candidate");
                });

            modelBuilder.Entity("RecruitmentManagementApi.Models.Entities.RecruitmentUpdateHistory", b =>
                {
                    b.HasOne("RecruitmentManagementApi.Models.Entities.Recruitment", "Recruitment")
                        .WithMany("RecruitmentUpdateHistories")
                        .HasForeignKey("RecruitmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Recruitment");
                });

            modelBuilder.Entity("RecruitmentManagementApi.Models.Entities.Candidate", b =>
                {
                    b.Navigation("Recruitment");
                });

            modelBuilder.Entity("RecruitmentManagementApi.Models.Entities.Recruitment", b =>
                {
                    b.Navigation("RecruitmentUpdateHistories");
                });
#pragma warning restore 612, 618
        }
    }
}

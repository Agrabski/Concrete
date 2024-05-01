﻿// <auto-generated />
using System;
using Concrete.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Concrete.Core.Data.Migrations
{
    [DbContext(typeof(ConcreteContext))]
    partial class ConcreteContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0-preview.3.24172.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Concrete.Core.Template.ActivityTemplate", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ClassTemplateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.HasKey("Id");

                    b.HasIndex("ClassTemplateId");

                    b.ToTable("ACTIVITY_TEMPLATE", (string)null);
                });

            modelBuilder.Entity("Concrete.Core.Template.ClassTemplate", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CourseTemplateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.HasKey("Id");

                    b.HasIndex("CourseTemplateId");

                    b.ToTable("CLASS_TEMPLATE", (string)null);
                });

            modelBuilder.Entity("Concrete.Core.Template.CourseTemplate", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.HasKey("Id");

                    b.ToTable("COURSE_TEMPLATE", (string)null);
                });

            modelBuilder.Entity("Concrete.Core.Template.ActivityTemplate", b =>
                {
                    b.HasOne("Concrete.Core.Template.ClassTemplate", null)
                        .WithMany("ActivityTemplates")
                        .HasForeignKey("ClassTemplateId");

                    b.OwnsOne("Concrete.Core.LocalisedText", "DisplayName", b1 =>
                        {
                            b1.Property<Guid>("ActivityTemplateId")
                                .HasColumnType("uniqueidentifier");

                            b1.HasKey("ActivityTemplateId");

                            b1.ToTable("ACTIVITY_TEMPLATE");

                            b1.ToJson("DisplayName");

                            b1.WithOwner()
                                .HasForeignKey("ActivityTemplateId");
                        });

                    b.Navigation("DisplayName")
                        .IsRequired();
                });

            modelBuilder.Entity("Concrete.Core.Template.ClassTemplate", b =>
                {
                    b.HasOne("Concrete.Core.Template.CourseTemplate", null)
                        .WithMany("ClassTemplates")
                        .HasForeignKey("CourseTemplateId");
                });

            modelBuilder.Entity("Concrete.Core.Template.ClassTemplate", b =>
                {
                    b.Navigation("ActivityTemplates");
                });

            modelBuilder.Entity("Concrete.Core.Template.CourseTemplate", b =>
                {
                    b.Navigation("ClassTemplates");
                });
#pragma warning restore 612, 618
        }
    }
}

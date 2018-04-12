﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using SouthAlive.Data;
using System;

namespace SouthAlive.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20180412014020_bando")]
    partial class bando
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("SouthAlive.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("DisplayName");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("SouthAlive.Models.BookingImages", b =>
                {
                    b.Property<int>("BookingImagesID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ImagePath");

                    b.Property<int>("VenueBookingID");

                    b.HasKey("BookingImagesID");

                    b.HasIndex("VenueBookingID");

                    b.ToTable("BookingImages");
                });

            modelBuilder.Entity("SouthAlive.Models.coordinateConstraint", b =>
                {
                    b.Property<int>("Key")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("X1");

                    b.Property<double>("X2");

                    b.Property<double>("Y1");

                    b.Property<double>("Y2");

                    b.HasKey("Key");

                    b.ToTable("coordinateConstraint");
                });

            modelBuilder.Entity("SouthAlive.Models.EventDetail", b =>
                {
                    b.Property<int>("EventDetailID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("VenueBookingID");

                    b.Property<string>("VenueLocation");

                    b.Property<string>("VenueName");

                    b.HasKey("EventDetailID");

                    b.HasIndex("VenueBookingID")
                        .IsUnique();

                    b.ToTable("EventDetail");
                });

            modelBuilder.Entity("SouthAlive.Models.LineInfo", b =>
                {
                    b.Property<int>("LineInfoID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("PhoneNumber")
                        .IsRequired();

                    b.Property<int>("PolyLineId");

                    b.HasKey("LineInfoID");

                    b.HasIndex("PolyLineId")
                        .IsUnique();

                    b.ToTable("LineInfo");
                });

            modelBuilder.Entity("SouthAlive.Models.NewsLetterPDF", b =>
                {
                    b.Property<int>("NewsLetterPDFID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Path")
                        .IsRequired();

                    b.Property<string>("Title");

                    b.HasKey("NewsLetterPDFID");

                    b.ToTable("NewsLetterPDF");
                });

            modelBuilder.Entity("SouthAlive.Models.PageIntroduction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Text")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("PageIntroduction");
                });

            modelBuilder.Entity("SouthAlive.Models.Polyline", b =>
                {
                    b.Property<int>("PolylineId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateAdded");

                    b.Property<DateTime>("LastChecked");

                    b.Property<string>("Notes");

                    b.Property<string>("PolyLine");

                    b.Property<string>("UserId");

                    b.HasKey("PolylineId");

                    b.HasIndex("UserId");

                    b.ToTable("Polyline");
                });

            modelBuilder.Entity("SouthAlive.Models.SampleEditor.Editor", b =>
                {
                    b.Property<int>("EditorId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content")
                        .HasColumnType("text");

                    b.Property<string>("Title");

                    b.HasKey("EditorId");

                    b.ToTable("Editor");
                });

            modelBuilder.Entity("SouthAlive.Models.Team", b =>
                {
                    b.Property<int>("TeamId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("TeamName")
                        .IsRequired();

                    b.HasKey("TeamId");

                    b.ToTable("Team");
                });

            modelBuilder.Entity("SouthAlive.Models.UserTeam", b =>
                {
                    b.Property<int>("UserTeamId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("TeamId");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("UserTeamId");

                    b.HasIndex("TeamId");

                    b.HasIndex("UserId");

                    b.ToTable("UserTeam");
                });

            modelBuilder.Entity("SouthAlive.Models.Venue", b =>
                {
                    b.Property<int>("VenueId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<bool>("IsSecret");

                    b.Property<int>("Price");

                    b.Property<string>("VenueName")
                        .IsRequired();

                    b.Property<int>("VenueTimesOptionsId");

                    b.HasKey("VenueId");

                    b.HasIndex("VenueTimesOptionsId");

                    b.ToTable("Venue");
                });

            modelBuilder.Entity("SouthAlive.Models.VenueBooking", b =>
                {
                    b.Property<int>("VenueBookingId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Day");

                    b.Property<string>("Description");

                    b.Property<TimeSpan>("EndTime");

                    b.Property<string>("EventName");

                    b.Property<bool>("IsApproved");

                    b.Property<bool>("IsFeatured");

                    b.Property<bool>("IsPublic");

                    b.Property<int>("Month");

                    b.Property<TimeSpan>("StartTime");

                    b.Property<string>("UserId");

                    b.Property<int>("VenueId");

                    b.Property<int>("Year");

                    b.HasKey("VenueBookingId");

                    b.HasIndex("UserId");

                    b.HasIndex("VenueId");

                    b.ToTable("VenueBooking");
                });

            modelBuilder.Entity("SouthAlive.Models.VenueCustomDay", b =>
                {
                    b.Property<int>("VenueCustomDayId")
                        .ValueGeneratedOnAdd();

                    b.Property<TimeSpan>("CloseTime");

                    b.Property<int>("Day");

                    b.Property<TimeSpan>("OpenTime");

                    b.Property<int>("VenueId");

                    b.HasKey("VenueCustomDayId");

                    b.HasIndex("VenueId");

                    b.ToTable("VenueCustomDay");
                });

            modelBuilder.Entity("SouthAlive.Models.VenueImages", b =>
                {
                    b.Property<int>("VenueImagesID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ImagePath");

                    b.Property<int>("VenueID");

                    b.HasKey("VenueImagesID");

                    b.HasIndex("VenueID");

                    b.ToTable("VenueImages");
                });

            modelBuilder.Entity("SouthAlive.Models.VenueTimesOptions", b =>
                {
                    b.Property<int>("VenueTimesOptionsId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("VenueTimesOptionsId");

                    b.ToTable("VenueTimesOptions");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("SouthAlive.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("SouthAlive.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SouthAlive.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("SouthAlive.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SouthAlive.Models.BookingImages", b =>
                {
                    b.HasOne("SouthAlive.Models.VenueBooking", "VenueBooking")
                        .WithMany("BookingImages")
                        .HasForeignKey("VenueBookingID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SouthAlive.Models.EventDetail", b =>
                {
                    b.HasOne("SouthAlive.Models.VenueBooking", "VenueBooking")
                        .WithOne("EventDetail")
                        .HasForeignKey("SouthAlive.Models.EventDetail", "VenueBookingID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SouthAlive.Models.LineInfo", b =>
                {
                    b.HasOne("SouthAlive.Models.Polyline", "PolyLine")
                        .WithOne("LineInfo")
                        .HasForeignKey("SouthAlive.Models.LineInfo", "PolyLineId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SouthAlive.Models.Polyline", b =>
                {
                    b.HasOne("SouthAlive.Models.ApplicationUser", "User")
                        .WithMany("Polylines")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("SouthAlive.Models.UserTeam", b =>
                {
                    b.HasOne("SouthAlive.Models.Team", "Team")
                        .WithMany("UserTeams")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SouthAlive.Models.ApplicationUser", "User")
                        .WithMany("UserTeams")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SouthAlive.Models.Venue", b =>
                {
                    b.HasOne("SouthAlive.Models.VenueTimesOptions", "VenueTimesOptions")
                        .WithMany("Venues")
                        .HasForeignKey("VenueTimesOptionsId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SouthAlive.Models.VenueBooking", b =>
                {
                    b.HasOne("SouthAlive.Models.ApplicationUser", "User")
                        .WithMany("VenueBookings")
                        .HasForeignKey("UserId");

                    b.HasOne("SouthAlive.Models.Venue", "Venue")
                        .WithMany("VenueBookings")
                        .HasForeignKey("VenueId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SouthAlive.Models.VenueCustomDay", b =>
                {
                    b.HasOne("SouthAlive.Models.Venue", "Venue")
                        .WithMany("VenueCustomDays")
                        .HasForeignKey("VenueId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SouthAlive.Models.VenueImages", b =>
                {
                    b.HasOne("SouthAlive.Models.Venue", "Venue")
                        .WithMany("VenueImages")
                        .HasForeignKey("VenueID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}

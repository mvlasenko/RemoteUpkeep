﻿using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Linq;

namespace RemoteUpkeep.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() : base("RemoteUpkeepContext", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public virtual DbSet<Service> Services { get; set; }

        public virtual DbSet<Order> Orders { get; set; }

        public virtual DbSet<OrderDetails> OrderDetails { get; set; }

        public virtual DbSet<Target> Targets { get; set; }

        public virtual DbSet<Action> Actions { get; set; }

        public virtual DbSet<Message> Messages { get; set; }

        public virtual DbSet<Image> Images { get; set; }

        public virtual DbSet<Country> Countries { get; set; }

        public virtual DbSet<Region> Regions { get; set; }

        public virtual DbSet<Language> Languages { get; set; }

        public virtual DbSet<Translation> Translations { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //users

            modelBuilder.Entity<ApplicationUser>()
                .HasOptional(e => e.Country)
                .WithMany(e => e.Users)
                .HasForeignKey(e => e.CountryId);

            modelBuilder.Entity<ApplicationUser>()
                .HasOptional(e => e.Region)
                .WithMany(e => e.Users)
                .HasForeignKey(e => e.RegionId);

            modelBuilder.Entity<ApplicationUser>()
                .HasOptional(e => e.PrimaryLanguage)
                .WithMany(e => e.Users2)
                .HasForeignKey(e => e.PrimaryLanguageId);

            //many-to-many
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(e => e.Languages)
                .WithMany(x => x.Users)
                .Map(m =>
                {
                    m.MapLeftKey("UserId");
                    m.MapRightKey("LanguageId");
                    m.ToTable("UserLanguages");
                });

            //regions

            modelBuilder.Entity<Region>()
                .HasRequired(e => e.Country)
                .WithMany(e => e.Regions)
                .HasForeignKey(e => e.CountryId);

            //services

            modelBuilder.Entity<Service>()
                .HasRequired(e => e.CreatedBy)
                .WithMany(e => e.ChangedServices)
                .HasForeignKey(e => e.CreatedByUserId)
                .WillCascadeOnDelete(false);

            //orders

            modelBuilder.Entity<Order>()
                .HasRequired(e => e.User)
                .WithMany(e => e.UserOrders)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);

            //order datails

            modelBuilder.Entity<OrderDetails>()
                .HasRequired(e => e.Target)
                .WithMany(e => e.OrderDetails)
                .HasForeignKey(e => e.TargetId);

            modelBuilder.Entity<OrderDetails>()
                .HasRequired(e => e.Order)
                .WithMany(e => e.OrderDetails)
                .HasForeignKey(e => e.OrderId);

            //many-to-many
            modelBuilder.Entity<OrderDetails>()
                .HasMany(e => e.Services)
                .WithMany(x => x.OrderDetails)
                .Map(m =>
                {
                    m.MapLeftKey("OrderDetailsId");
                    m.MapRightKey("ServiceId");
                    m.ToTable("OrderDetailsServices");
                });

            //targets

            modelBuilder.Entity<Target>()
                .HasRequired(e => e.Region)
                .WithMany(e => e.Targets)
                .HasForeignKey(e => e.RegionId);

            modelBuilder.Entity<Target>()
                .HasOptional(e => e.ChangedBy)
                .WithMany(e => e.ChangedTargets)
                .HasForeignKey(e => e.ChangedByUserId);

            //many-to-many
            modelBuilder.Entity<Target>()
                .HasMany(e => e.Images)
                .WithMany(x => x.Targets)
                .Map(m =>
                {
                    m.MapLeftKey("TargetId");
                    m.MapRightKey("ImageId");
                    m.ToTable("TargetImages");
                });

            //actions

            modelBuilder.Entity<Action>()
                .HasRequired(e => e.OrderDetails)
                .WithMany(e => e.Actions)
                .HasForeignKey(e => e.OrderDetailsId);

            modelBuilder.Entity<Action>()
                .HasOptional(e => e.AssignedUser)
                .WithMany(e => e.AssignedActions)
                .HasForeignKey(e => e.AssignedUserId);

            modelBuilder.Entity<Action>()
                .HasOptional(e => e.ChangedBy)
                .WithMany(e => e.ChangedActions)
                .HasForeignKey(e => e.ChangedByUserId);

            //messages

            modelBuilder.Entity<Message>()
                .HasOptional(e => e.Sender)
                .WithMany(e => e.SentMessages)
                .HasForeignKey(e => e.SenderId);

            modelBuilder.Entity<Message>()
                .HasRequired(e => e.Receiver)
                .WithMany(e => e.ReceivedMessages)
                .HasForeignKey(e => e.ReceiverId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Message>()
                .HasOptional(e => e.Language)
                .WithMany(e => e.Messages)
                .HasForeignKey(e => e.LanguageId);

            modelBuilder.Entity<Message>()
                .HasRequired(e => e.OrderDetails)
                .WithMany(e => e.Messages)
                .HasForeignKey(e => e.OrderDetailsId);

            //many-to-many
            modelBuilder.Entity<Message>()
                .HasMany(e => e.Attachments)
                .WithMany(x => x.Messages)
                .Map(m =>
                {
                    m.MapLeftKey("MessageId");
                    m.MapRightKey("ImageId");
                    m.ToTable("MessageAttachments");
                });

            //translations

            modelBuilder.Entity<Translation>()
                .HasRequired(e => e.Language)
                .WithMany(e => e.Translations)
                .HasForeignKey(e => e.LanguageId);

        }

        public override int SaveChanges()
        {
            var modifiedEntities = ChangeTracker.Entries().ToList();
            return base.SaveChanges();
        }
    }
}
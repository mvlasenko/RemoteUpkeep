using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

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

        public virtual DbSet<Target> Targets { get; set; }

        public virtual DbSet<Location> Locations { get; set; }

        public virtual DbSet<Action> Actions { get; set; }

        public virtual DbSet<Message> Messages { get; set; }

        public virtual DbSet<Image> Images { get; set; }

        public virtual DbSet<Country> Countries { get; set; }

        public virtual DbSet<Region> Regions { get; set; }

        public virtual DbSet<Language> Languages { get; set; }


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

            //services

            modelBuilder.Entity<Service>()
                .HasRequired(e => e.CreatedBy)
                .WithMany(e => e.ChangedServices)
                .HasForeignKey(e => e.CreatedByUserId)
                .WillCascadeOnDelete(false);

            //many-to-many
            modelBuilder.Entity<Service>()
                .HasMany(e => e.Orders)
                .WithMany(x => x.Services)
                .Map(m =>
                {
                    m.MapLeftKey("ServiceId");
                    m.MapRightKey("OrderId");
                    m.ToTable("ServiceOrders");
                });

            //orders

            modelBuilder.Entity<Order>()
                .HasRequired(e => e.Target)
                .WithMany(e => e.Orders)
                .HasForeignKey(e => e.TargetId);

            modelBuilder.Entity<Order>()
                .HasRequired(e => e.User)
                .WithMany(e => e.UserOrders)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);

            //targets

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

            //many-to-many
            modelBuilder.Entity<Target>()
                .HasMany(e => e.Messages)
                .WithMany(x => x.Targets)
                .Map(m =>
                {
                    m.MapLeftKey("TargetId");
                    m.MapRightKey("MessageId");
                    m.ToTable("TargetMessages");
                });

            modelBuilder.Entity<Target>()
                .HasRequired(e => e.Region)
                .WithMany(e => e.Targets)
                .HasForeignKey(e => e.RegionId);

            modelBuilder.Entity<Target>()
                .HasOptional(e => e.Location)
                .WithMany(e => e.Targets)
                .HasForeignKey(e => e.LocationId);

            modelBuilder.Entity<Target>()
                .HasOptional(e => e.ChangedBy)
                .WithMany(e => e.ChangedTargets)
                .HasForeignKey(e => e.ChangedByUserId);

            //actions

            modelBuilder.Entity<Action>()
                .HasRequired(e => e.Target)
                .WithMany(e => e.Actions)
                .HasForeignKey(e => e.TargetId);

            modelBuilder.Entity<Action>()
                .HasOptional(e => e.AssignedUser)
                .WithMany(e => e.AssignedActions)
                .HasForeignKey(e => e.AssignedUserId);

            modelBuilder.Entity<Action>()
                .HasOptional(e => e.ChangedBy)
                .WithMany(e => e.ChangedActions)
                .HasForeignKey(e => e.ChangedByUserId);

            //locations

            modelBuilder.Entity<Location>()
                .HasOptional(e => e.ChangedBy)
                .WithMany(e => e.ChangedLocations)
                .HasForeignKey(e => e.ChangedByUserId);

            //messages

            modelBuilder.Entity<Message>()
                .HasRequired(e => e.Sender)
                .WithMany(e => e.SentMessages)
                .HasForeignKey(e => e.SenderId);

            modelBuilder.Entity<Message>()
                .HasRequired(e => e.Receiver)
                .WithMany(e => e.ReceivedMessages)
                .HasForeignKey(e => e.ReceiverId)
                .WillCascadeOnDelete(false);

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

        }
    }
}
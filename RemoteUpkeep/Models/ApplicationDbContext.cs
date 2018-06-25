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
                .WithMany(e => e.Services)
                .HasForeignKey(e => e.CreatedByUserId);

            //orders

            modelBuilder.Entity<Order>()
                .HasRequired(e => e.User)
                .WithMany(e => e.Orders)
                .HasForeignKey(e => e.UserId);

            //targets

            //many-to-many
            modelBuilder.Entity<Target>()
                .HasMany(e => e.Services)
                .WithMany(x => x.Targets)
                .Map(m =>
                {
                    m.MapLeftKey("TargetId");
                    m.MapRightKey("ServiceId");
                    m.ToTable("ServiceTargets");
                });

            //many-to-many
            modelBuilder.Entity<Target>()
                .HasMany(e => e.Messages)
                .WithMany(x => x.Targets)
                .Map(m =>
                {
                    m.MapLeftKey("TargetId");
                    m.MapRightKey("MessageId");
                    m.ToTable("MessageTargets");
                });

            modelBuilder.Entity<Target>()
                .HasRequired(e => e.Location)
                .WithMany(e => e.Targets)
                .HasForeignKey(e => e.LocationId);

            //actions

            modelBuilder.Entity<Action>()
                .HasRequired(e => e.Target)
                .WithMany(e => e.Actions)
                .HasForeignKey(e => e.TargetId);

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

            //images

            //many-to-many
            modelBuilder.Entity<Image>()
                .HasMany(e => e.Messages)
                .WithMany(x => x.Attachments)
                .Map(m =>
                {
                    m.MapLeftKey("ImageId");
                    m.MapRightKey("MessageId");
                    m.ToTable("Attachments");
                });

            //many-to-many
            modelBuilder.Entity<Image>()
                .HasMany(e => e.Targets)
                .WithMany(x => x.Images)
                .Map(m =>
                {
                    m.MapLeftKey("ImageId");
                    m.MapRightKey("TargetId");
                    m.ToTable("TargetImages");
                });

        }

    }
}
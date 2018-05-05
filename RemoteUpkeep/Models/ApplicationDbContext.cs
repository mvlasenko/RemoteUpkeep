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

        public virtual DbSet<Object> Objects { get; set; }

        public virtual DbSet<Location> Locations { get; set; }

        public virtual DbSet<Action> Actions { get; set; }

        public virtual DbSet<Message> Messages { get; set; }

        public virtual DbSet<MessageAttachment> MessageAttachments { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //services

            modelBuilder.Entity<Service>()
                .HasRequired(e => e.CreatedBy)
                .WithMany(e => e.Services)
                .HasForeignKey(e => e.Id);

            //orders

            modelBuilder.Entity<Order>()
                .HasRequired(e => e.User)
                .WithMany(e => e.Orders)
                .HasForeignKey(e => e.Id);

            //objects

            modelBuilder.Entity<Object>()
                .HasRequired(e => e.Service)
                .WithMany(e => e.Objects)
                .HasForeignKey(e => e.Id);

            modelBuilder.Entity<Object>()
                .HasRequired(e => e.Location)
                .WithMany(e => e.Objects)
                .HasForeignKey(e => e.Id);

            //actions

            modelBuilder.Entity<Action>()
                .HasRequired(e => e.Object)
                .WithMany(e => e.Actions)
                .HasForeignKey(e => e.Id);

            //messages

            modelBuilder.Entity<Message>()
                .HasRequired(e => e.Sender)
                .WithMany(e => e.SentMessages)
                .HasForeignKey(e => e.Id);

            modelBuilder.Entity<Message>()
                .HasRequired(e => e.Receiver)
                .WithMany(e => e.ReceivedMessages)
                .HasForeignKey(e => e.Id);

            modelBuilder.Entity<Message>()
                .HasRequired(e => e.Object)
                .WithMany(e => e.Messages)
                .HasForeignKey(e => e.Id);

            //message attachments

            modelBuilder.Entity<MessageAttachment>()
                .HasRequired(e => e.Message)
                .WithMany(e => e.Attachments)
                .HasForeignKey(e => e.Id);

        }

    }
}
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

        public virtual DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Message>()
                .HasRequired(e => e.User)
                .WithMany(e => e.Messages)
                .HasForeignKey(e => e.Id);
        }

    }
}
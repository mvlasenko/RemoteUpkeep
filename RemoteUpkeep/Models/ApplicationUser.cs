using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace RemoteUpkeep.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            this.Languages = new List<Language>();

            this.SentMessages = new List<Message>();
            this.ReceivedMessages = new List<Message>();

            //client-specific
            this.UserOrders = new List<Order>();

            //admin-specific
            this.ChangedServices = new List<Service>();
            this.ChangedTargets = new List<Target>();
            this.ChangedActions = new List<Action>();
            this.ChangedLocations = new List<Location>();

            //dealer-specific
            this.AssignedActions = new List<Action>();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public UserType UserType { get; set; }

        public int? CountryId { get; set; }

        public virtual Country Country { get; set; }

        public int? RegionId { get; set; }

        public virtual Region Region { get; set; }

        public string FullName
        {
            get
            {
                return this.FirstName + " " + this.LastName;

            }
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public virtual ICollection<Language> Languages { get; set; }

        public virtual ICollection<Message> SentMessages { get; set; }

        public virtual ICollection<Message> ReceivedMessages { get; set; }

        public virtual ICollection<Order> UserOrders { get; set; }

        public virtual ICollection<Service> ChangedServices { get; set; }

        public virtual ICollection<Target> ChangedTargets { get; set; }

        public virtual ICollection<Action> ChangedActions { get; set; }

        public virtual ICollection<Location> ChangedLocations { get; set; }

        public virtual ICollection<Action> AssignedActions { get; set; }

    }
}
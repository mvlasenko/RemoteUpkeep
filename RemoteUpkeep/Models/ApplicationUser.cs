using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            this.ChangedOrders = new List<Order>();
            this.ChangedActions = new List<Action>();

            //dealer-specific
            this.AssignedActions = new List<Action>();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Display(Name = "Type")]
        public UserType UserType { get; set; }

        [UIHint("_Country")]
        [Display(Name = "Country")]
        public int? CountryId { get; set; }

        public virtual Country Country { get; set; }

        [UIHint("_Region")]
        [Display(Name = "Region")]
        public int? RegionId { get; set; }

        public virtual Region Region { get; set; }

        [Display(Name = "Full Name")]
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

        public virtual ICollection<Order> ChangedOrders { get; set; }

        public virtual ICollection<Action> ChangedActions { get; set; }

        public virtual ICollection<Action> AssignedActions { get; set; }

    }
}
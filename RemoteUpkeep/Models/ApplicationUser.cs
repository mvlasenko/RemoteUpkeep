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
            this.SentMessages = new List<Message>();
            this.ReceivedMessages = new List<Message>();
            this.Orders = new List<Order>();
            this.Services = new List<Service>();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public UserType UserType { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public virtual ICollection<Message> SentMessages { get; set; }

        public virtual ICollection<Message> ReceivedMessages { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public virtual ICollection<Service> Services { get; set; }

    }
}
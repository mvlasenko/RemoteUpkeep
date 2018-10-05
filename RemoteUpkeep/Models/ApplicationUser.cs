using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RemoteUpkeep.Validation;

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

            //dealer-specific
            this.AssignedActions = new List<Action>();
        }

        [Display(Name = "FirstName", ResourceType = typeof(Properties.Resources))]
        public string FirstName { get; set; }

        [Display(Name = "LastName", ResourceType = typeof(Properties.Resources))]
        public string LastName { get; set; }

        [UIHint("_Country")]
        [Display(Name = "Country", ResourceType = typeof(Properties.Resources))]
        public int? CountryId { get; set; }

        public virtual Country Country { get; set; }

        [UIHint("_Region")]
        [Display(Name = "Region", ResourceType = typeof(Properties.Resources))]
        public int? RegionId { get; set; }

        public virtual Region Region { get; set; }

        [UIHint("_Language")]
        [Display(Name = "PrimaryLanguage", ResourceType = typeof(Properties.Resources))]
        public int? PrimaryLanguageId { get; set; }

        public virtual Language PrimaryLanguage { get; set; }

        [Display(Name = "FullName", ResourceType = typeof(Properties.Resources))]
        public string FullName
        {
            get
            {
                return this.FirstName + (string.IsNullOrEmpty(this.LastName) ? "" : " " + this.LastName);
            }
        }

        //view only
        [NotMapped]
        [Display(Name = "Languages", ResourceType = typeof(Properties.Resources))]
        [UIHint("_Languages")]
        [CheckboxListRequired]
        public ICollection<int> LanguageIds { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        [ScriptIgnore(ApplyToOverrides = true)]
        public virtual ICollection<Language> Languages { get; set; }

        [ScriptIgnore(ApplyToOverrides = true)]
        public virtual ICollection<Message> SentMessages { get; set; }

        [ScriptIgnore(ApplyToOverrides = true)]
        public virtual ICollection<Message> ReceivedMessages { get; set; }

        [ScriptIgnore(ApplyToOverrides = true)]
        public virtual ICollection<Order> UserOrders { get; set; }

        [ScriptIgnore(ApplyToOverrides = true)]
        public virtual ICollection<Service> ChangedServices { get; set; }

        [ScriptIgnore(ApplyToOverrides = true)]
        public virtual ICollection<Target> ChangedTargets { get; set; }

        [ScriptIgnore(ApplyToOverrides = true)]
        public virtual ICollection<Action> ChangedActions { get; set; }

        [ScriptIgnore(ApplyToOverrides = true)]
        public virtual ICollection<Action> AssignedActions { get; set; }

        //base json ignores

        [ScriptIgnore(ApplyToOverrides = true)]
        public override ICollection<IdentityUserClaim> Claims => base.Claims;

        [ScriptIgnore(ApplyToOverrides = true)]
        public override ICollection<IdentityUserLogin> Logins => base.Logins;

        [ScriptIgnore(ApplyToOverrides = true)]
        public override ICollection<IdentityUserRole> Roles => base.Roles;

        [ScriptIgnore(ApplyToOverrides = true)]
        public override string PasswordHash
        {
            get { return base.PasswordHash; }
            set { base.PasswordHash = value; }
        }

        [ScriptIgnore(ApplyToOverrides = true)]
        public override string SecurityStamp
        {
            get { return base.SecurityStamp; }
            set { base.SecurityStamp = value; }
        }

        [ScriptIgnore(ApplyToOverrides = true)]
        public override bool EmailConfirmed
        {
            get { return base.EmailConfirmed; }
            set { base.EmailConfirmed = value; }
        }

        [ScriptIgnore(ApplyToOverrides = true)]
        public override bool PhoneNumberConfirmed
        {
            get { return base.PhoneNumberConfirmed; }
            set { base.PhoneNumberConfirmed = value; }
        }

        [ScriptIgnore(ApplyToOverrides = true)]
        public override bool TwoFactorEnabled
        {
            get { return base.TwoFactorEnabled; }
            set { base.TwoFactorEnabled = value; }
        }

        [ScriptIgnore(ApplyToOverrides = true)]
        public override bool LockoutEnabled
        {
            get { return base.LockoutEnabled; }
            set { base.LockoutEnabled = value; }
        }

        [ScriptIgnore(ApplyToOverrides = true)]
        public override DateTime? LockoutEndDateUtc
        {
            get { return base.LockoutEndDateUtc; }
            set { base.LockoutEndDateUtc = value; }
        }

        [ScriptIgnore(ApplyToOverrides = true)]
        public override int AccessFailedCount
        {
            get { return base.AccessFailedCount; }
            set { base.AccessFailedCount = value; }
        }
    }
}
namespace MvcTemplate.Web
{
    using System;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin;
    using Microsoft.Owin.Security.DataProtection;

    using MvcTemplate.Data;
    using MvcTemplate.Data.Models;

    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(
            IUserStore<ApplicationUser> store,
            IdentityFactoryOptions<ApplicationUserManager> options)
            : base(store)
        {
            // Configure validation logic for usernames
            this.UserValidator = new UserValidator<ApplicationUser>(this)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true,
            };

            // Configure validation logic for passwords
            this.PasswordValidator = new PasswordValidator
            {
                RequiredLength = Common.GlobalConstants.PasswordMinLength,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };

            // Configure user lockout defaults
            this.UserLockoutEnabledByDefault = true;
            this.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            this.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            this.RegisterPhoneNumberProvider();
            this.RegisterEmailProvider();
            this.RegisterDataProtectionProvider(options.DataProtectionProvider);

            this.EmailService = new EmailService();
            this.SmsService = new SmsService();
        }

        private void RegisterPhoneNumberProvider()
        {
            var provider = new PhoneNumberTokenProvider<ApplicationUser>
            {
                MessageFormat = "Your security code is {0}",
            };
            this.RegisterTwoFactorProvider("Phone Code", provider);
        }

        private void RegisterEmailProvider()
        {
            var provider = new EmailTokenProvider<ApplicationUser>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}",
            };
            this.RegisterTwoFactorProvider("Email Code", provider);
        }

        private void RegisterDataProtectionProvider(IDataProtectionProvider provider)
        {
            if (provider != null)
            {
                this.UserTokenProvider =
                    new DataProtectorTokenProvider<ApplicationUser>(provider.Create("ASP.NET Identity"));
            }
        }
    }
}

// This document was provided by our professor in the Secure Software Development course at Mohawk College.
// By no means do I take credit for the creation of this script. I did edit the implmenetation to meet the
// required means of our assignment.

// I, Nicholas Cassar, student number 000902104, certify that this material is my
// original work. No other person's work has been used without due
// acknowledgement and I have not made my work available to anyone else.
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab1_SecureSoftware_NicholasCassar.Models;

namespace Lab1_SecureSoftware_NicholasCassar.Data
{
    public static class DbInitializer
    {

        /// <summary>
        /// SeedUsersAndRoles() - 
        /// This method calls the appropriate methods to populate the database in the appropriate
        /// sequence of steps. This method handles the creation of the required services, and all
        /// method calls required to successfully seed the database with user roles and users.
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns>An int representing a failure corrosponding the the method called or 0 for success.
        /// </returns>
        public static async Task<int> SeedUsersAndRoles(IServiceProvider serviceProvider)
        {
            // create the database if it doesn't exist
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            context.Database.Migrate();

            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            // Check if roles already exist and exit if there are
            if (roleManager.Roles.Count() > 0)
                return 1;  // should log an error message here

            // Seed roles
            int result = await SeedRoles(roleManager);
            if (result != 0)
                return 2;  // should log an error message here

            // Check if users already exist and exit if there are
            if (userManager.Users.Count() > 0)
                return 3;  // should log an error message here

            // Seed users
            result = await SeedUsers(userManager);
            if (result != 0)
                return 4;  // should log an error message here

            return 0;
        }

        /// <summary>
        /// SeedRoles()
        /// This method is incharge of seeding user roles within the database.
        /// </summary>
        /// <param name="roleManager">An api accessor for managing roles.</param>
        /// <returns>An int representing a success or failure regarding this functions execution.
        /// </returns>
        private static async Task<int> SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            // Create Supervisor Role
            var result = await roleManager.CreateAsync(new IdentityRole("Supervisor"));
            if (!result.Succeeded)
                return 1;  // should log an error message here

            // Create Employee Role
            result = await roleManager.CreateAsync(new IdentityRole("Employee"));
            if (!result.Succeeded)
                return 2;  // should log an error message here

            return 0;
        }



        /// <summary>
        /// SeedUsers()
        /// This method is incharge of seeding Users within the database.
        /// </summary>
        /// <param name="userManager">An api accessor for managing users.</param>
        /// <returns>An int representing a success or failure regarding this functions execution.
        /// </returns>
        private static async Task<int> SeedUsers(UserManager<ApplicationUser> userManager)
        {
            // Create Supervisor User
            var adminUser = new ApplicationUser
            {
                UserName = "the.supervisor@mohawkcollege.ca",
                Email = "the.supervisor@mohawkcollege.ca",
                FirstName = "The",
                LastName = "supervisor",
                EmailConfirmed = true,
                City = "Hamilton"
            };
            var result = await userManager.CreateAsync(adminUser, "Password!1");
            if (!result.Succeeded)
                return 1;  // should log an error message here

            // Assign user to Supervisor role
            result = await userManager.AddToRoleAsync(adminUser, "Supervisor");
            if (!result.Succeeded)
                return 2;  // should log an error message here

            // Create Employee User
            var memberUser = new ApplicationUser
            {
                UserName = "the.employee@mohawkcollege.ca",
                Email = "the.employee@mohawkcollege.ca",
                FirstName = "The",
                LastName = "employee",
                EmailConfirmed = true,
                City = "Hamilton"

            };
            result = await userManager.CreateAsync(memberUser, "Password!1");
            if (!result.Succeeded)
                return 3;  // should log an error message here

            // Assign user to Employee role
            result = await userManager.AddToRoleAsync(memberUser, "Employee");
            if (!result.Succeeded)
                return 4;  // should log an error message here

            return 0;
        }
    }
}

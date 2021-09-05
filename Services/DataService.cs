using BlogProject.Data;
using BlogProject.Enums;
using BlogProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogProject.Services
{
    public class DataService
    {
        // Necessary to talk to DB
        private readonly ApplicationDbContext _dbContext;

        //Needed to talk to the role manager
        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly UserManager<BlogUser> _userManager;

        public DataService(ApplicationDbContext dbContext, 
                            RoleManager<IdentityRole> roleManager, 
                            UserManager<BlogUser> userManager)
        {
            _dbContext = dbContext;
            _roleManager = roleManager;
            _userManager = userManager;
        }


        public async Task ManageDataAsync()
        {
            //Task 0: Create the DB from the migrations
            await _dbContext.Database.MigrateAsync();

            // Task 1: Seed a few roles into the system
            await SeedRolesAsync();

            // Task 2: Seed a few users into the system 
            await SeedUsersAsync();

        }

        private async Task SeedRolesAsync()
        {
            //If there are already roles in the system, do nothing
            if (_dbContext.Roles.Any())
            {
                return;
            }
            //Otherwise, create a few roles
            foreach(var role in Enum.GetNames(typeof(BlogRole)))
            {
                //use the role manager to create roles
                await _roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        private async Task SeedUsersAsync()
        {
            //If there are already users in the system, do nothing
            if (_dbContext.Users.Any())
            {
                return;
            }
            //Otherwise, create a few users
            //Step 1: Creates a new instance of Bloguser
            var adminUser = new BlogUser()
            {
                Email = "Simonvdfliert@hotmail.com",
                UserName = "Simonvdfliert",
                FirstName = "Simon",
                LastName = "van de Fliert",
                PhoneNumber = "(800) 555-1212",
                EmailConfirmed = true
            };

            //Step 2: Use the Usermanager to create a new user that is defined by adminUser
            await _userManager.CreateAsync(adminUser, "Abc&123!");

            //Step 3: Add this new user to the Administrator role
            await _userManager.AddToRoleAsync(adminUser, BlogRole.Administrator.ToString());

            // Step 1 repeat: Create a moderator user
            var modUser = new BlogUser()
            {
                Email = "Alexvdfliert@hotmail.com",
                UserName = "Alexvdfliert",
                FirstName = "Alex",
                LastName = "van de Fliert",
                PhoneNumber = "(800) 555-2323",
                EmailConfirmed = true
            };

            //Step 2 repeat: Use the usermanager to create a new user that is defined by adminUser
            await _userManager.CreateAsync(modUser, "Abc&123!");

            //Step 3 repeat : Add this new user to the Administrator role
            await _userManager.AddToRoleAsync(modUser, BlogRole.Moderator.ToString());

        }


        
    }
}

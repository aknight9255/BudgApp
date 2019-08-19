using System;
using System.Collections.Generic;
using System.Linq;
using BudgApp.Data.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;

[assembly: OwinStartup(typeof(BudgApp.API.Startup))]

namespace BudgApp.API
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            ConfigureAuth(app);
            createRolesandUsers();
        }

        //in this method we create default User roles and Admin user for login 
        private void createRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            //In startup iam creating first Admin role and creating a default Admin User 
            if (!roleManager.RoleExists("Admin"))
            {
                //first we create admin role
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                //Here we create a Admin super user who will maintain the website                  

                var user = new ApplicationUser();
                user.Email = "admin@admin.admin";

                string userPWD = "Admin1!";

                var checkUser = UserManager.Create(user, userPWD);

                //Add default User to Role Admin   
                if (checkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Admin");

                }

            }
        }
    }
}

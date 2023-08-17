using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using MovieApp.Areas.Identity.Data;
using MovieApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace MovieApp.Seed
{
    public class DbInitializer : IdbInitializer
    {
        private readonly MovieAppContext _context;
        private readonly UserManager<MovieAppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(MovieAppContext context, UserManager<MovieAppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task Initialize()
        {
            try
            {
                if (_context.Database.GetPendingMigrations().Count() > 0)
                {
                    _context.Database.Migrate();
                }
            }
            catch (Exception)
            {
                throw;
            }

            if (!_roleManager.RoleExistsAsync(UserRole.Admin.ToString()).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(UserRole.Admin.ToString())).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(UserRole.User.ToString())).GetAwaiter().GetResult();



            }
            var x = new MovieAppUser();

            var user = new MovieAppUser
            {
               PhoneNumber ="9803345656",
                PhoneNumberConfirmed = true,
                Email = "movies@admin.com",
                UserName = "movies@admin.com",
                NormalizedEmail = "MOVIES@ADMIN.COM",
                NormalizedUserName = "MOVIES@ADMIN.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D"),
            };
            var userManager = _userManager.CreateAsync(user,"Admin@123").GetAwaiter().GetResult();
            var result = _context.Users.FirstOrDefault(a => a.Email == "movies@admin.com");
            _userManager.AddToRoleAsync(user, UserRole.Admin.ToString()).GetAwaiter().GetResult();
            await _context.SaveChangesAsync();
        }
    }
}

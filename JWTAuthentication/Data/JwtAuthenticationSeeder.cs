using JWTAuthentication.Entities;
using Microsoft.AspNetCore.Identity;

namespace JWTAuthentication.Data
{
    public class JwtAuthenticationSeeder
    {
        private static readonly List<(User, string)> users = new()
        {
            (
                new User
                {
                    Id = "3b333929-f974-444e-a8d3-68f50a0459c0",
                    UserName = "User1",
                    FirstName = "Arturia",
                    LastName = "Giallo",
                    Email = "user1@mail.com"
                },
                "User%password1"
            ),
            (
                new User
                {
                    Id = "61dfb9e3-1c27-424a-9963-9586ca110220",
                    UserName = "User2",
                    FirstName = "Ostap",
                    LastName = "Bleier",
                    Email = "user2@mail.com"
                },
                "User%password2"
            ),
            (
                new User
                {
                    Id = "a36b02e1-81a9-47f4-89b6-d33c4f40ed5f",
                    UserName = "User3",
                    FirstName = "Federico",
                    LastName = "Giallo",
                    Email = "user3@mail.com"
                },
                "User%password3"
            ),
            (
                new User
                {
                    Id = "013a2014-4a25-4a3d-9fae-e0f783d42ef9",
                    UserName = "User4",
                    FirstName = "Franz",
                    LastName = "von Urtica",
                    Email = "user4@mail.com"
                },
                "User%password4"
            ),
            (
                new User
                {
                    Id = "ae557ffc-2906-4913-bd26-40aa98a55570",
                    UserName = "User5",
                    FirstName = "Vlasi",
                    LastName = "Arterberry",
                    Email = "user5@mail.com"
                },
                "User%password5"
            ) 
        };

        public static async Task SeedEssentialsAsync(UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            //Seed Roles
            await roleManager.CreateAsync(new IdentityRole(Roles.Administrator.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Moderator.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.User.ToString()));

            foreach (var (user, password) in users)
            {
                //user.PasswordHash = new PasswordHasher<User>().HashPassword(user, password);
                await userManager.CreateAsync(user, password);
                await userManager.AddToRoleAsync(user, Roles.User.ToString());
            }
        }
    };
}

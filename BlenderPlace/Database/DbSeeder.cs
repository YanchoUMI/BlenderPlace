using Microsoft.AspNetCore.Identity;

namespace BlenderPlace.Database
{
    public class DbSeeder
    {
        public static async Task SeedRolesAndAdminAsync(IServiceProvider service)
        {
            //Seed Roles
            var userManager = service.GetService<UserManager<ApplicationUser>>();
            var roleManager = service.GetService<RoleManager<IdentityRole>>();

            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Creator.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.User.ToString()));

            // creating admin

            var user = new ApplicationUser
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                Name = "Yancho",
                FirstName = "Yancho",
                LastName = "Ibrahimov",
                PhoneNumber = "0000000000",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            var user1 = new ApplicationUser
            {
                UserName = "toziotlqvo@gmail.com",
                Email = "toziotlqvo@gmail.com",
                Name = "Tozi",
                FirstName = "Tozi",
                LastName = "Otlqvo",
                PhoneNumber = "0000000001",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            var defaultAdmin = await userManager.FindByEmailAsync(user.Email);
            if (defaultAdmin == null)
            {
                await userManager.CreateAsync(user, "Admin123!");
                await userManager.AddToRoleAsync(user, Roles.Admin.ToString());
            }

            var thatGuy = await userManager.FindByEmailAsync(user1.Email);
            if (thatGuy == null)
            {
                await userManager.CreateAsync(user1, "Tozi123!");
                await userManager.AddToRoleAsync(user1, Roles.Creator.ToString());
            }
        }

        public async Task TutorialSeed(ApplicationDbContext context, IServiceProvider provider)
        {
            if (context.Tutorials.Any())
            {
                return;
            }
            var userManager = provider.GetService<UserManager<ApplicationUser>>();

            var defaultCreator = await userManager.FindByEmailAsync("toziotlqvo@gmail.com");
            var admin = await userManager.FindByEmailAsync("admin@gmail.com");
            var tutorials = new List<Tutorial>
            {
                new Tutorial
                {
                    Title = "Topology Made Easy",
                    CreatedByName = "Tozi",
                    URL = "https://blog.yarsalabs.com/content/images/2022/04/quad-to-quad.png",
                    Description = "In this tutorial, you'll learn the fundamentals of creating good topology in Blender, which is essential for modeling, sculpting, and animation. The focus will be on understanding edge flow, maintaining clean meshes, and ensuring the geometry is both efficient and easy to work with. You'll explore tools and techniques like the extrude, merge, and knife tools, as well as tips for optimizing the mesh for smooth deformations during animation. ",
                    Category = Category.Modeling,
                    CreateDate = new DateTime(2025, 2, 5),
                    Creator = defaultCreator!
                },
                new Tutorial
                {
                    Title = "Master Basic Animations",
                    CreatedByName = "Tozi",
                    URL = "https://code.blender.org/wp-content/uploads/2022/11/Blender-Animation-Layer-Mock-Up-Multi-Character.png?x39017",
                    Description = "This tutorial introduces the basics of animation in Blender, perfect for beginners. You'll learn how to bring your models to life by understanding keyframes, the timeline, and how to animate objects or characters. Starting with simple movements, we'll cover essential techniques such as setting keyframes, using the Graph Editor to fine-tune animations, and applying interpolation for smooth motion.",
                    Category = Category.Animation,
                    CreateDate = new DateTime(2025, 1, 12),
                    Creator = defaultCreator!
                },
                new Tutorial
                {
                    Title = "Bring Your Ideas to Life",
                    CreatedByName = "Tozi",
                    URL = "https://cdnb.artstation.com/p/media_assets/images/images/001/017/753/large/clip-2.jpg?1668166164",
                    Description = "This tutorial covers the basics of sculpting in Blender, guiding you through essential tools and techniques for creating detailed, organic models. You'll learn how to use brushes, dynamic topology, and masks to shape your meshes, giving you the foundation to create intricate designs and characters.",
                    Category = Category.Sculpting,
                    CreateDate = new DateTime(2025, 1, 28),
                    Creator = defaultCreator!
                },
                new Tutorial
                {
                    Title = "Integrate AI Tools in Your Animation Process",
                    Category = Category.Animation,
                    Description = "You'll learn how to integrate AI (shock, I know) into your animations in Blender, using Mixamo.",
                    CreatedByName = "Yancho",
                    CreateDate = DateTime.Now,
                    URL = "https://code.blender.org/wp-content/uploads/2024/02/bone_picker.webp?x39017",
                    Creator = admin!
                },
                new Tutorial
                {
                    Title = "Know All the Modifiers in Blender",
                    Category = Category.Modeling,
                    Description = "Manipulate the mesh with 50+ modifiers.",
                    CreatedByName = "Yancho",
                    CreateDate = DateTime.Now,
                    URL = "https://public-files.gumroad.com/tv28yf9nsxzn9p7w4lv7bq4ptbqq",
                    Creator = admin!
                },
                new Tutorial
                {
                    Title = "Create Your Own Textures With Shading Nodes",
                    Category = Category.Shading,
                    Description = "You'll never guess what you'll do here.",
                    CreatedByName = "Yancho",
                    CreateDate = DateTime.Now,
                    URL = "https://s3.amazonaws.com/cgcookie-rails/uploads%2F1598553133983-1598553133983.png",
                    Creator = admin!
                },
                new Tutorial
                {
                    Title = "Generate Objects With Geometry Nodes",
                    Category = Category.Geometry,
                    Description = "Description, mf, do you speak it?",
                    CreatedByName = "Yancho",
                    CreateDate = DateTime.Now,
                    URL = "https://i.all3dp.com/workers/images/fit=scale-down,w=1200,h=675,gravity=0.5x0.5,format=auto/wp-content/uploads/2022/07/12164724/abc-xyz-blender-220712.jpg",
                    Creator = admin!
                }
            };

            context.Tutorials.AddRange(tutorials);
            context.SaveChanges();
        }
    }
}

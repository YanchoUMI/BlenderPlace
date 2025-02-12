using Microsoft.AspNetCore.Identity;

namespace BlenderPlace.Database
{
    public class ApplicationUser : IdentityUser
    {
        public string? Name { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public List<Tutorial> FavoriteTutorials { get; set; } = new List<Tutorial>();
    }
}

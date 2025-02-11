using System.ComponentModel.DataAnnotations;

namespace BlenderPlace.Database
{
    public enum Roles
    {
        [Display(Name = "Admin")]
        Admin,

        [Display(Name = "Creator")]
        Creator,

        [Display(Name = "User")]
        User
    }
}

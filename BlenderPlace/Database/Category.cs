using System.ComponentModel.DataAnnotations;

namespace BlenderPlace.Database
{
    public enum Category
    {
        [Display(Name = "Modeling")]
        Modeling,

        [Display(Name = "Animation")]
        Animation,

        [Display(Name = "Shading")]
        Shading,

        [Display(Name = "Geometry")]
        Geometry,

        [Display(Name = "Sculpting")]
        Sculpting
    }
}

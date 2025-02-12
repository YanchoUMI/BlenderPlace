using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace BlenderPlace.Database
{
    public class Tutorial
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public string URL { get; set; }

        [Required(ErrorMessage = "Description is mandatory")]
        public string Description { get; set; }


        [EnumDataType(typeof(Category))]
        [Required(ErrorMessage = "Category is mandatory")]
        public Category Category { get; set; }

        [ValidateNever]
        public string? CreatorId { get; set; }
        [ValidateNever]
        public ApplicationUser Creator { get; set; }

        public DateTime CreateDate { get; set; }

        [ValidateNever]
        public string CreatedByName { get; set; }

        [ValidateNever]
        public List<ApplicationUser> FavoritedByUsers { get; set; } = new List<ApplicationUser>();
    }
}

using System.ComponentModel.DataAnnotations;

namespace Test.Models
{
    public class PersonModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(255, ErrorMessage = "Last name must be less then 255 letters")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [StringLength(255, ErrorMessage = "First name must be less then 255 letters")]
        public string FirstName { get; set; }

        public int? Value { get; set; }
    }
}
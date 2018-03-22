using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Services.Models
{
    public class PersonResult
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(255, ErrorMessage = "Last name must be less then 255 charecters")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [StringLength(255, ErrorMessage = "First name must be less then 255 charecters")]
        public string FirstName { get; set; }

        public int? Value { get; set; }
    }
}

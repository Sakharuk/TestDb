using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using Test.DataAccess.BaseInterfaces;

namespace Test.DataAccess
{
    [Table("People")]
    public partial class Person : IEntity
    {
        [Key]
        public int PeopleID { get; set; }

        [Required]
        [StringLength(255)]
        public string LastName { get; set; }

        [Required]
        [StringLength(255)]
        public string FirstName { get; set; }

        public virtual Mark Mark { get; set; }
    }
}

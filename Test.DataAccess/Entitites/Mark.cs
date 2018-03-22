using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Test.DataAccess.BaseInterfaces;

namespace Test.DataAccess
{
    [Table("Mark")]
    public partial class Mark : IEntity
    {
        [Key]
        public int PeopleID { get; set; }

        public int? Value { get; set; }

        public virtual Person Person { get; set; }
    }
}

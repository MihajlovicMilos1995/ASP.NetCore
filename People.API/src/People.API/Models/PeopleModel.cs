using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace People.API.Models
{
    public class PeopleModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [MaxLength (13)]
        public long Jmbg { get; set; }

        public string Gender { get; set; }

        public string Occupation { get; set; }
    }
}

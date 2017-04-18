using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace People.API.Models
{
    public class JobModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string JobTitle { get; set; }
        public string CompanyName { get; set; }

        [ForeignKey("PersonId")]
        public PeopleModel Person { get; set; }
        public string PersonId { get; set; }

    }
}

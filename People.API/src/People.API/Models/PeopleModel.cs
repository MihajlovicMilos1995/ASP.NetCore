using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace People.API.Models
{
    public class PeopleModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Jmbg { get; set; }
        public string Gender { get; set; }
        public string Occupation { get; set; }
    }
}

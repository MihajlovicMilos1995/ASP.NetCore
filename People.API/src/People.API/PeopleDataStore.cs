using System;
using People.API.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace People.API
{
    public class PeopleDataStore
    {
        public static PeopleDataStore Current { get; } = new PeopleDataStore();

        public List<PeopleModel> People { get; set; }

        public PeopleDataStore()
        {
            People = new List<PeopleModel>()
            {
              new PeopleModel()
            {
                Id = 1,
                FirstName = "Milos",
                LastName = "Mihajlovic",
                Jmbg = 1231231,
                Gender = "male",
                Occupation = "smurf"
            },

            new PeopleModel()
            {
                Id = 2,
                FirstName = "fsadad",
                LastName = "sdsada",
                Jmbg = 123124,
                Gender = "female",
                Occupation = "bad driver"
            }
            };
        }
    }
}

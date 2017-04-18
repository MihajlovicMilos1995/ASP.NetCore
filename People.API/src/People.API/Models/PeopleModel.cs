using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace People.API.Models
{
    public class PeopleModel
    {
        public PeopleModel()
        {

        }

        private string firstName;

        private string lastName;

        private string jmbg;

        private string gender;


        public string FirstName
        {
            get
            {
                return firstName;
            }

            set
            {
                firstName = value;
            }
        }

        public string LastName
        {
            get
            {
                return lastName;
            }

            set
            {
                lastName = value;
            }
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Jmbg must be numeric")]
        public string Jmbg
        {
            get
            {
                return jmbg;
            }

            set
            {
                jmbg = value;
            }
        }

        public string Gender
        {
            get
            {
                return gender;
            }

            set
            {
                gender = value;
            }
        }

        public  ICollection<JobModel> Job { get; set; }
    }
}

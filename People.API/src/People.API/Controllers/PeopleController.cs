using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace People.API.Controllers
{
    [Route("api/people")]
    public class PeopleController : Controller
    {
        [HttpGet()]
        public JsonResult GetPeople()
        {
            return new JsonResult(PeopleDataStore.Current.People);
        }

        [HttpGet("{id}")]
        public JsonResult GetPerson(int id)
        {
            return new JsonResult(
                PeopleDataStore.Current.People.FirstOrDefault(c => c.Id == id)
                );
        }
    }
}

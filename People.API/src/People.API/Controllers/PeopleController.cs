using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using People.API.Models;
using System.Collections.Generic;
using System.Linq;

namespace People.API.Controllers
{
    [Route("api/[controller]")]
    public class PeopleController : Controller
    {
        private PeopleContext _ctx;
        private UserManager<IdentityUser> _userManager;

        public PeopleController(PeopleContext ctx, UserManager<IdentityUser> UserManager)
        {
            _ctx = ctx;
            _userManager = UserManager;
        }

        [HttpPost("adduser")]
        public IActionResult AddUser()
        {
            var user = new IdentityUser()
            {
                UserName = "Ultradumb",
                Email = "milos@people.com"
            };

            var id = _userManager.CreateAsync(user, "P@ssw0rd!").Result;
            return new NoContentResult();
        }

        [AllowAnonymous]
        [HttpGet("getAll")]
        public IEnumerable<PeopleModel> GetAllPeople()
        {
            return _ctx.People.ToList();
        }

        public PeopleModel GetPerson(string Jmbg, bool includeJob)
        {
            if (includeJob)
            {
                return _ctx.People.Include(c => c.Job)
                    .Where(c => c.Jmbg == Jmbg).FirstOrDefault();
            }
            return _ctx.People.Where(c => c.Jmbg == Jmbg).FirstOrDefault();
        }

        [AllowAnonymous]
        [HttpGet("getById/{Jmbg}")]
        public IActionResult GetById(string Jmbg, bool showJob)
        {
                return new ObjectResult(GetPerson(Jmbg, showJob));
        }

        [Authorize]
        [HttpPost("createPerson")]
        public IActionResult Create([FromBody] PeopleModel person)
        {
            if (person == null)
            {
                return BadRequest();
            }
            _ctx.Add(person);
            _ctx.SaveChanges();

            return Created("api/people", person);
        }

        //[Authorize]
        [HttpPut("update/{Jmbg}")]
        public IActionResult Update(string Jmbg, [FromBody] PeopleModel updatePerson)
        {
            if (updatePerson == null || updatePerson.Jmbg != Jmbg)
            {
                return BadRequest();
            }
            var todo = _ctx.People.Find(Jmbg);
            if (todo == null)
            {
                return NotFound();
            }

            todo.FirstName = updatePerson.FirstName;
            todo.LastName = updatePerson.LastName;
            todo.Jmbg = updatePerson.Jmbg;
            todo.Gender = updatePerson.Gender;
            todo.Job = updatePerson.Job;

            _ctx.SaveChanges();
            return new NoContentResult();
        }

        [Authorize]
        [HttpDelete("delete/{Jmbg}")]
        public IActionResult Delete(long Jmbg)
        {
            var todo = _ctx.People.Find(Jmbg);
            if (todo == null)
            {
                return NotFound();
            }
            _ctx.People.Remove(todo);
            _ctx.SaveChanges();

            return new NoContentResult();
        }

        [HttpGet("person")]
        public IActionResult SearchAndSort([FromQuery]string searchString, [FromQuery] string sortBy, [FromQuery] int page, [FromQuery] int peoplePerPage)
        {
            var people = from p in _ctx.People
                         select p;

            if (searchString != null)
            {
                people = people.Where(p => p.FirstName.Contains(searchString)
                                        || p.LastName.Contains(searchString));
            }
            string sortOrder = sortBy;
            if (sortOrder == "de")
            {
                people = people.OrderByDescending(p => p.FirstName);
            }
            else if (sortOrder == "asc")
            {
                people = people.OrderBy(p => p.FirstName);
            }

            peoplePerPage = 4;
            if (page > 0)
            {
                people = people.Skip((page - 1) * peoplePerPage).Take(peoplePerPage);
            }

            return Ok(people);
        }
    }
}

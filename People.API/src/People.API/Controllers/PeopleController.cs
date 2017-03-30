using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using People.API.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task EnsureSeedData()
        {
            if (await _userManager.FindByEmailAsync("mihajlovicmilos16@gmai.com") == null)
            {
                var user = new IdentityUser()
                {
                    UserName = "Ultradumb",
                    Email = "mihajlovicmilos16@gmail.com"
                };
                await _userManager.CreateAsync(user, "P@ssw0rd!");
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public IEnumerable<PeopleModel> GetAll()
        {
            return _ctx.People.ToList();
        }

        [AllowAnonymous]
        [HttpGet("{Jmbg}", Name = "GetBy")]
        public IActionResult GetById(long Jmbg)
        {
            var person = _ctx.People.Find(Jmbg);
            if (person == null)
            {
                return NotFound();
            }
            return new ObjectResult(person);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create([FromBody] PeopleModel person)
        {
            if (person == null)
            {
                return BadRequest();
            }
            _ctx.Add(person);
            _ctx.SaveChanges();

            return CreatedAtRoute("GetPeople",
                new { jmbg = person.Jmbg }, person);
        }

        [Authorize]
        [HttpPut("{Jmbg}")]
        public IActionResult Update(long Jmbg, [FromBody] PeopleModel updatePerson)
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
            todo.Occupation = updatePerson.Occupation;

            _ctx.SaveChanges();

            return new NoContentResult();
        }

        [Authorize]
        [HttpDelete("{Jmbg}")]
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
    }
}

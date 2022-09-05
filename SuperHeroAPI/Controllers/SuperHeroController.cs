using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperHeroAPI.Data;
using SuperHeroAPI.Models;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {

        private static List<SuperHero> heroes = new List<SuperHero>
            {
                new Models.SuperHero
                {
                    Id = 1,
                    Name = "Wolverine",
                    FirstName = "James",
                    LastName = "Howlett",
                    Location = " Cold Lake, Alberta"
                },
                 new Models.SuperHero
                {
                    Id = 2,
                    Name = "Spiderman",
                    FirstName = "Peter",
                    LastName = "Parker",
                    Location = "Manhattan"
                }
            };
       
        private readonly DataContext context;

        public SuperHeroController(DataContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get() // IActionResult wont return example in swagger
        {
            return Ok(await context.SuperHeroes.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> Get(int id) // pass in id
        {
            var hero = await context.SuperHeroes.FindAsync(id); // use find method to match id
            if (hero == null) // if no matches...
                return BadRequest("Hero not found"); // then return bad request
            return Ok(hero); //otherwise return the hero that matches the condition
        }


        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero) // IActionResult wont return example in swagger
        {
            context.SuperHeroes.Add(hero);
            await context.SaveChangesAsync();

            return Ok(await context.SuperHeroes.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero request) // IActionResult wont return example in swagger
        {
            var hero = await context.SuperHeroes.FindAsync(request.Id); // use find method to match id
            if (hero == null) // if no matches...
                return BadRequest("Hero not found"); // then return bad request

            hero.Name = request.Name;
            hero.FirstName = request.FirstName;
            hero.LastName = request.LastName;
            hero.Location =  request.Location;

            await context.SaveChangesAsync();

            return Ok(await context.SuperHeroes.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperHero>>> Delete(int id) // IActionResult wont return example in swagger
        {
            var hero = await context.SuperHeroes.FindAsync(id); // use find method to match id
            if (hero == null) // if no matches...
                return BadRequest("Hero not found"); // then return bad request
            
            context.SuperHeroes.Remove(hero);
            await context.SaveChangesAsync();

            return Ok(await context.SuperHeroes.ToListAsync());
        }


    }
}

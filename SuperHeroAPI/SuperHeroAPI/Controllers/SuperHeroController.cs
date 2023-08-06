using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperHeroAPI.Dto;
using SuperHeroAPI.Models;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly DataContext _context;

        public SuperHeroController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult> GetSuperHero()
        {
            var hero = await _context.SuperHeroes.ToListAsync();
            return Ok(hero);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSuperHero(SuperHeroDTO dto)
        {
            var SuperHero = new SuperHero { 
            Name = dto.Name,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Place = dto.Place,
            };
            await _context.AddAsync(SuperHero);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpadteSuperHero(int id,[FromBody]SuperHeroDTO dto)
        {
            var hero = await _context.SuperHeroes.SingleOrDefaultAsync(h => h.Id == id);

            if(hero == null)
            {
                return NotFound($"Not superHero found by this Id");
            }
            hero.Name = dto.Name;
            hero.FirstName = dto.FirstName;
            hero.LastName = dto.LastName;
            hero.Place = dto.Place;

            _context.SaveChanges();
            return Ok(hero);
            
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSuperHero (int id)
        {
            var hero = await _context.SuperHeroes.SingleOrDefaultAsync(h => h.Id == id);

            if (hero == null)
            {
                return NotFound($"Not superHero found by this Id");
            }

            _context.Remove(hero);
            _context.SaveChanges();
            return Ok(hero);
        }
    }
}

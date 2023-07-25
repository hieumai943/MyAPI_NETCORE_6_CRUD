using Microsoft.AspNetCore.Mvc;
using MyAPINetCore6.Models;
using MyAPINetCore6.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyAPINetCore6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHumanController : ControllerBase
    {
        private readonly IHeroRepository _herorepo;

        public SuperHumanController(IHeroRepository herorepo) {
            _herorepo = herorepo;
        }
        // GET: api/<SuperHumanController>
        [HttpGet]
        public async Task<IActionResult> GetAllHero()
        {
            try
            {
                return Ok(await _herorepo.GetAllHerosAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetHeroById(int id)
        {
            var hero = _herorepo.GetHerosAsync(id);
            return hero == null ? NotFound() : Ok(hero);
        }
        [HttpPost]
        public async Task<IActionResult> AddNewHero(HeroModel hero)
        {
                var newHeroId = await _herorepo.AddHeroAsync(hero); // vi addheroasync tra ve id 
                var _hero = _herorepo.GetHerosAsync(newHeroId);
                return _hero == null ? NotFound() : Ok(_hero);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHero(int id, HeroModel hero)
        {
            if (id != hero.Id) return NotFound();
            await _herorepo.UpdateHeroAsync(id, hero);
            return Ok();

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHero(int id)
        {
            await _herorepo.DeleteHeroAsync(id);
            return Ok();
        }

    }
}

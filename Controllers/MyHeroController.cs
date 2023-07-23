using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyAPINetCore6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyHeroController : ControllerBase
    {
       
        private DataContext _context;
        public MyHeroController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<MyHero>> Get()
        {
            return Ok( await _context.Heroes.ToListAsync());
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<MyHero>> Get(int id)
        {
            var hero = await _context.Heroes.FindAsync( id);
            if (hero == null) return BadRequest("Not Found ID");
            return Ok(hero);
        }
        [HttpPost]
        public async Task<ActionResult<List<MyHero>>> AddHero(MyHero hero)
        {
            _context.Heroes.Add(hero);
            await _context.SaveChangesAsync();  
            return Ok(await _context.Heroes.ToListAsync());
        }     
        [HttpPut]
        public async Task<ActionResult<List<MyHero>>> UpdateHero(MyHero request)
        {
            var hero = await _context.Heroes.FindAsync(request.Id);
            if (hero == null) return BadRequest("Node found ID");   
            hero.Name = request.Name;
            hero.FirstName = request.FirstName;
            hero.LastName = request.LastName;
            hero.Place = request.Place;

            await _context.SaveChangesAsync();


            return Ok(await _context.Heroes.ToListAsync());  
        }
        [HttpDelete]
        public async Task<ActionResult<List<MyHero>>> DeleteHero(MyHero request)
        {
            var hero = await _context.Heroes.FindAsync(request.Id);

            if (hero == null) return BadRequest("Not found ID");

            _context.Heroes.Remove(hero);
            await _context.SaveChangesAsync();

            return Ok(await _context.Heroes.ToListAsync());
        }
    }
}

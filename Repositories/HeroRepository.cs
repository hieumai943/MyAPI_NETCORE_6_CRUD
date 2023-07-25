using AutoMapper;
using Azure.Core;
using MyAPINetCore6.Models;

namespace MyAPINetCore6.Repositories
{
    public class HeroRepository : IHeroRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public HeroRepository(DataContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;

        }

        public async Task<int> AddHeroAsync(HeroModel hero)
        {
            var newHero = _mapper.Map<MyHero>(hero);
            _context.Heroes.Add(newHero);
            await _context.SaveChangesAsync();
            return newHero.Id;
        }

        public async Task DeleteHeroAsync(int id)
        {
            var hero = await _context.Heroes.FindAsync(id);
            if (hero != null)
            {
                _context.Heroes.Remove(hero);
                await _context.SaveChangesAsync();

            }

        }

        public async Task<List<HeroModel>> GetAllHerosAsync()
        {
           var heroes = await _context.Heroes.ToListAsync();
            return _mapper.Map<List<HeroModel>>(heroes); // tuc la cai nay la chuyen
        }

        public async Task<HeroModel> GetHerosAsync(int id)
        {
            var heroes = await _context.Heroes.FindAsync(id);
            return _mapper.Map<HeroModel>(heroes); // tuc la cai Map<heromodel> la chuyen tu entity sang model
        }

        public async Task UpdateHeroAsync(int id, HeroModel hero)
        {
            if (id == hero.Id)
            {
                var updatehero = _mapper.Map<MyHero>(hero);

                _context.Heroes.Update(updatehero);
                await _context.SaveChangesAsync();
            }
        }
    }
}

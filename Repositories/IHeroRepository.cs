using MyAPINetCore6.Models;

namespace MyAPINetCore6.Repositories
{
    public interface IHeroRepository
    {
        public Task<List<HeroModel>> GetAllHerosAsync();
        public Task<HeroModel> GetHerosAsync(int id);
        public Task<int> AddHeroAsync(HeroModel hero);
        public Task UpdateHeroAsync(int id, HeroModel hero);
        public Task DeleteHeroAsync(int id);

    }
}

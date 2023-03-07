using PokemonAPI.Contexts;
using PokemonLibrary;

namespace PokemonAPI.Repositories
{
    public class PokemonsRepositoryDB : IPokemonsRepository
    {
        private PokemonContext _context;

        public PokemonsRepositoryDB(PokemonContext context)
        {
            _context = context;
        }

        public Pokemon Add(Pokemon newPokemon)
        {
            newPokemon.Id = 0;
            _context.pokemons.Add(newPokemon);
            _context.SaveChanges();
            return newPokemon;
        }

        public Pokemon? Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Pokemon> GetAll(int? amount, string? namefilter)
        {
            return _context.pokemons.ToList();
        }

        public Pokemon? GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public Pokemon? Update(int id, Pokemon updates)
        {
            throw new NotImplementedException();
        }
    }
}

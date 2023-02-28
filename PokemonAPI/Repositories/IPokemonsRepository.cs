using PokemonAPI.Models;

namespace PokemonAPI.Repositories
{
    public interface IPokemonsRepository
    {
        Pokemon Add(Pokemon newPokemon);
        Pokemon? Delete(int id);
        List<Pokemon> GetAll(int? amount, string? namefilter);
        Pokemon? GetByID(int id);
        Pokemon? Update(int id, Pokemon updates);
    }
}
using Microsoft.EntityFrameworkCore;
using PokemonAPI.Models;

namespace PokemonAPI.Contexts
{
    public class PokemonContext : DbContext
    {
        public PokemonContext(DbContextOptions<PokemonContext>
            options) : base(options) { }

        public DbSet<Pokemon> pokemons { get; set; }
    }
}

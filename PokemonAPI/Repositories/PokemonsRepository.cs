using PokemonAPI.Models;


namespace PokemonAPI.Repositories
{
    public class PokemonsRepository
    {
        private int _nextID;
        private List<Pokemon> _pokemons;

        public PokemonsRepository() 
        {
            _nextID = 1;
            _pokemons = new List<Pokemon>()
            {
                new Pokemon() {Id = _nextID++, Name="Pikachu", Level=9999, PokeDex=25},
                new Pokemon() {Id = _nextID++, Name="Charmander", Level=1000, PokeDex=12},
                new Pokemon() {Id = _nextID++, Name="Arbok", Level=20, PokeDex=80},
            };
        }

        public List<Pokemon> GetAll()
        {
            return new List<Pokemon>(_pokemons);
        }

        public Pokemon Add(Pokemon newPokemon)
        {
            newPokemon.Id = _nextID++;
            _pokemons.Add(newPokemon);
            return newPokemon;
        }
    }
}

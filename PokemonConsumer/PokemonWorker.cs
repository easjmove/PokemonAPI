using PokemonLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PokemonConsumer
{
    public class PokemonWorker
    {
        private JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };

        public void DoWork()
        {
            List<Pokemon>? pokemons = GetAll();
            foreach (Pokemon pokemon in pokemons)
            {
                Console.WriteLine(pokemon);
            }
        }

        public List<Pokemon>? GetAll()
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync(
                    "https://pokemonapi20230216124134.azurewebsites.net/" +
                    "api/pokemons").Result;
                string json = response.Content.ReadAsStringAsync().Result;
                List<Pokemon>? list = JsonSerializer.Deserialize
                    <List<Pokemon>>(json, options);
                return list;
            }
        }
    }
}

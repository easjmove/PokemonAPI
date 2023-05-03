using PokemonLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PokemonConsumer
{
    public class PokemonWorker
    {
        private static readonly string URL = "https://pokemonapi20230216124134.azurewebsites.net/api/pokemons";

        private readonly JsonSerializerOptions options = new JsonSerializerOptions
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
                HttpResponseMessage response = client.GetAsync(URL).Result;
                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.OK:
                        string json = response.Content.ReadAsStringAsync().Result;
                        List<Pokemon>? list = JsonSerializer.Deserialize<List<Pokemon>>(json, options);
                        return list;
                    case System.Net.HttpStatusCode.NoContent:
                        return null;
                    default:
                        throw new Exception("Unknown error occured: " + response.StatusCode);
                }
            }
        }

        public Pokemon? GetById(int Id)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync(URL + "/" + Id).Result;
                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.OK:
                        string json = response.Content.ReadAsStringAsync().Result;
                        Pokemon? foundPokemon = JsonSerializer.Deserialize<Pokemon>(json, options);
                        return foundPokemon;
                    case System.Net.HttpStatusCode.NotFound:
                        return null;
                    default:
                        throw new Exception("Unknown error occured: " + response.StatusCode);
                }
            }
        }

        public Pokemon Post(Pokemon newPokemon)
        {
            using (HttpClient client = new HttpClient())
            {
                JsonContent content = JsonContent.Create(newPokemon);
                HttpResponseMessage response = client.PostAsync(URL, content).Result;

                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.Created:
                        string json = response.Content.ReadAsStringAsync().Result;
                        Pokemon? createdPokemon = JsonSerializer.Deserialize<Pokemon>(json, options);
                        if (createdPokemon == null)
                        {
                            throw new Exception("Unknown error occured, received null!");
                        }
                        return createdPokemon;
                    case System.Net.HttpStatusCode.BadRequest:
                        throw new ArgumentException("Server responded with BadRequest");
                    default:
                        throw new Exception("Unknown error occured: " + response.StatusCode);
                }
            }
        }

        public Pokemon? Put(int Id, Pokemon newPokemon)
        {
            using (HttpClient client = new HttpClient())
            {
                JsonContent content = JsonContent.Create(newPokemon);
                HttpResponseMessage response = client.PutAsync(URL + "/" + Id, content).Result;

                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.OK:
                        string json = response.Content.ReadAsStringAsync().Result;
                        Pokemon? updatedPokemon = JsonSerializer.Deserialize<Pokemon>(json, options);
                        return updatedPokemon;
                    case System.Net.HttpStatusCode.NotFound:
                        throw new ArgumentNullException("Server responded with NotFound");
                    case System.Net.HttpStatusCode.BadRequest:
                        throw new ArgumentException("Server responded with BadRequest");
                    default:
                        throw new Exception("Unknown error occured: " + response.StatusCode);
                }
            }
        }

        public Pokemon? Delete(int Id)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = client.DeleteAsync(URL + "/" + Id).Result;
                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.OK:
                        string json = response.Content.ReadAsStringAsync().Result;
                        Pokemon? deletedPokemon = JsonSerializer.Deserialize<Pokemon>(json, options);
                        return deletedPokemon;
                    case System.Net.HttpStatusCode.NotFound:
                        return null;
                    default:
                        throw new Exception("Unknown error occured: " + response.StatusCode);
                }
            }
        }
    }
}

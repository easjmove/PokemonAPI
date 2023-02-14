using Microsoft.AspNetCore.Mvc;
using PokemonAPI.Models;
using PokemonAPI.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PokemonAPI.Controllers
{
    [Route("api/[controller]")]
    //URI: api/pokemons
    [ApiController]
    public class PokemonsController : ControllerBase
    {
        private PokemonsRepository _repository;

        public PokemonsController(PokemonsRepository repository)
        {
            _repository = repository;
        }

        // GET: api/<PokemonsController>
        [HttpGet]
        public IEnumerable<Pokemon> Get()
        {
            return _repository.GetAll();
        }

        // GET api/<PokemonsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<PokemonsController>
        [HttpPost]
        public Pokemon Post([FromBody] Pokemon newPokemon)
        {
            return _repository.Add(newPokemon);
        }

        // PUT api/<PokemonsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PokemonsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

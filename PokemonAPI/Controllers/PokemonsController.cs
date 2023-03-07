using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PokemonAPI.Repositories;
using PokemonLibrary;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PokemonAPI.Controllers
{
    [EnableCors("AllowAll")]
    [Route("api/[controller]")]
    //URI: api/pokemons
    [ApiController]
    public class PokemonsController : ControllerBase
    {
        private IPokemonsRepository _repository;

        public PokemonsController(IPokemonsRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Pokemons?minlevel=1&namefilter=har
        [HttpGet]
        public ActionResult<IEnumerable<Pokemon>> GetAll(
            [FromHeader] int? amount,
            [FromQuery] string? namefilter, 
            [FromQuery] int? minlevel)
        {
            List<Pokemon> result = _repository.GetAll(amount,namefilter);
            if (result.Count < 1)
            {
                return NoContent(); // NotFound er også ok
            }
            Response.Headers.Add("TotalAmount", "" + result.Count());
            return Ok(result);
        }

        // GET api/Pokemons/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public ActionResult<Pokemon> Get(int id)
        {
            Pokemon? foundPokemon = _repository.GetByID(id);
            if (foundPokemon == null)
            {
                return NotFound();
            }
            return Ok(foundPokemon);
        }

        // POST api/<PokemonsController>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public ActionResult<Pokemon> Post([FromBody] Pokemon newPokemon)
        {
            try
            {
                Pokemon createdPokemon = _repository.Add(newPokemon);
                return Created($"api/pokemons/{createdPokemon.Id}", createdPokemon);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<PokemonsController>/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]
        public ActionResult<Pokemon> Put(int id, [FromBody] Pokemon updates)
        {
            try
            {
                Pokemon? updatedPokemon = _repository.Update(id, updates);
                if (updatedPokemon == null)
                {
                    return NotFound();
                }
                return Ok(updatedPokemon);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<PokemonsController>/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public ActionResult<Pokemon> Delete(int id)
        {
            Pokemon? deletedPokemon = _repository.Delete(id);
            if (deletedPokemon == null)
            {
                return NotFound();
            }
            return Ok(deletedPokemon);
        }
    }
}

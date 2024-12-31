using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateController : ControllerBase
    {
        private readonly StateRepository _stateRepository;
        private readonly CityRepository _cityRepository;

        public StateController(StateRepository stateRepository, CityRepository cityRepository)
        {
            _stateRepository = stateRepository;
            _cityRepository = cityRepository;
        }

        [HttpGet]
        public IActionResult GetAllStates()
        {
            var states = _stateRepository.GetAll();
            return Ok(states);
        }

        [HttpGet("{id}")]
        public IActionResult GetStateById(int id)
        {
            var state = _stateRepository.GetById(id);
            if (state == null)
            {
                return NotFound();
            }
            return Ok(state);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteState(int id)
        {
            var isDeleted = _stateRepository.Delete(id);
            if (!isDeleted)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPost]
        public IActionResult InsertState([FromBody] StateModel state)
        {
            if (state == null)
            {
                return BadRequest();
            }
            bool isInserted = _stateRepository.Insert(state);
            if (isInserted)
                return Ok(new { Message = "State Inserted Successfully!" });
            return StatusCode(500, "An error occurred while inserting the state.");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateState(int id, [FromBody] StateModel state)
        {
            if (state == null || id != state.StateID)
            {
                return BadRequest();
            }

            var isUpdated = _stateRepository.Update(state);
            if (!isUpdated)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}

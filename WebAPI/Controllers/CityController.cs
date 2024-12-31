using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly CityRepository _cityRepository;

        public CityController(CityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        [HttpGet]
        public IActionResult GetAllCities()
        {
            var cities = _cityRepository.GetAll();
            return Ok(cities);
        }

        [HttpGet("{id}")]
        public IActionResult GetCityById(int id)
        {
            var city = _cityRepository.GetById(id);
            if (city == null)
            {
                return NotFound();
            }
            return Ok(city);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCity(int id)
        {
            var isDeleted = _cityRepository.Delete(id);
            if (!isDeleted)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPost]
        public IActionResult InsertCity([FromBody] CityModel city)
        {
            if (city == null)
            {
                return BadRequest();
            }
            bool isInserted = _cityRepository.Insert(city);
            if (isInserted)
                return Ok(new { Message = "City Inserted Successfully!" });
            return StatusCode(500, "An error occurred while inserting the city.");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCity(int id, [FromBody] CityModel city)
        {
            if (city == null || id != city.CityID)
            {
                return BadRequest();
            }

            var isUpdated = _cityRepository.Update(city);
            if (!isUpdated)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpGet("countries")]
        public IActionResult GetCountries()
        {
            var countries = _cityRepository.GetCountries();
            if (!countries.Any())
                return NotFound("No countries found.");

            return Ok(countries);
        }

        [HttpGet("states/{countryID}")]
        public IActionResult GetStatesByCountryID(int countryID)
        {
            if (countryID <= 0)
                return BadRequest("Invalid Country ID.");

            var states = _cityRepository.GetStatesByCountryID(countryID);
            if (!states.Any())
                return NotFound("No states found for the given Country ID.");

            return Ok(states);
        }

    }
}

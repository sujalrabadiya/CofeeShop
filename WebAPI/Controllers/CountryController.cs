using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly CountryRepository _countryRepository;

        public CountryController(CountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        [HttpGet]
        public IActionResult GetAllCountries()
        {
            var countries = _countryRepository.GetAll();
            return Ok(countries);
        }

        [HttpGet("{id}")]
        public IActionResult GetCountryById(int id)
        {
            var country = _countryRepository.GetById(id);
            if (country == null)
            {
                return NotFound();
            }
            return Ok(country);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCountry(int id)
        {
            var isDeleted = _countryRepository.Delete(id);
            if (!isDeleted)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPost]
        public IActionResult InsertCountry([FromBody] CountryModel country)
        {
            if (country == null)
            {
                return BadRequest();
            }
            bool isInserted = _countryRepository.Insert(country);
            if (isInserted)
                return Ok(new { Message = "Country Inserted Successfully!" });
            return StatusCode(500, "An error occurred while inserting the country.");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCountry(int id, [FromBody] CountryModel country)
        {
            if (country == null || id != country.CountryID)
            {
                return BadRequest();
            }

            var isUpdated = _countryRepository.Update(country);
            if (!isUpdated)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}

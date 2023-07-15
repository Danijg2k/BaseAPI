using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BaseAPI.Context;
using BaseAPI.Models;
using BaseAPI.Models.Dto;

namespace BaseAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class CitiesController : ControllerBase
    {
        private readonly CityContext _context;

        public CitiesController(CityContext context)
        {
            _context = context;
        }

        // GET: api/Cities

        /// <summary>
        /// Get all
        /// </summary>
        /// <returns>list of [cities]</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET api/Cities
        ///     
        /// </remarks>
        /// <response code="200">Always returned (if data does or doesn't exist)</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CityDto>))]
        public async Task<IActionResult> GetCities()
        {
            return Ok(await _context.Cities
                .Select(x => ItemToDTO(x))
                .ToListAsync());
        }

        // GET: api/Cities/5

        /// <summary>
        /// Get by Id
        /// </summary>
        /// <param name="id">id to the [city] to retrieve</param>
        /// <returns>[city] with given id, if exists</returns>
        /// <remarks>
        /// Sample request:    
        ///    
        ///     GET api/Cities/1
        ///     
        /// </remarks>
        /// <response code="200">If item with given id is retrieved</response>
        /// <response code="404">If item with given id doesn't exist</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CityDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCity(int id)
        {
            var city = await _context.Cities.FindAsync(id);

            if (city == null)
            {
                return NotFound();
            }

            return Ok(ItemToDTO(city));
        }

        // PUT: api/Cities/5

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="id">id of the [city] to update</param>
        /// <param name="cityDto">new data for the updated [city]</param>
        /// <remarks>
        /// Sample request:    
        ///    
        ///     PUT api/Cities/1
        ///     {
        ///         "id": 1,
        ///         "name": "Osaka",
        ///         "population": 19013434,
        ///         "isCapital": true
        ///     }
        /// 
        /// </remarks>
        /// <response code="204">If the item is updated</response>
        /// <response code="400">If the item is null / If id from url and id from body aren't the same</response>
        /// <response code="404">If both ids are equal but don't belong to an existing item</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutCity(int id, CityDto cityDto)
        {
            if (id != cityDto.Id)
            {
                throw new BadHttpRequestException("Ids of given item and url don't match");
                //return BadRequest();
            }

            var city = await _context.Cities.FindAsync(id);

            if (city == null)
            {
                return NotFound();
            }

            city.Name = cityDto.Name;
            city.Population = cityDto.Population;
            city.IsCapital = cityDto.IsCapital;

            _context.Entry(city).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Cities

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="cityDto">data of the [city] to add</param>
        /// <returns>created [city]</returns>
        /// /// <remarks>
        /// Sample request:    
        ///    
        ///     POST api/Cities
        ///     {
        ///         "name": "Paris",
        ///         "population": 11208440,
        ///         "isCapital": true
        ///     }
        /// 
        /// </remarks>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CityDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostCity(CityDto cityDto)
        {
            var city = new City
            {
                Name = cityDto.Name,
                Population = cityDto.Population,
                IsCapital = cityDto.IsCapital
            };

            _context.Cities.Add(city);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCity), new { id = city.Id }, ItemToDTO(city));
        }


        // DELETE: api/Cities/5

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id">id of the [city] to delete</param>
        /// <remarks>
        /// Sample request:    
        ///    
        ///     DELETE api/Cities/1
        ///     
        /// </remarks>
        /// <response code="204">If the item is deleted</response>
        /// <response code="404">If the item is not found</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCity(int id)
        {
            var city = await _context.Cities.FindAsync(id);
            if (city == null)
            {
                return NotFound();
            }

            _context.Cities.Remove(city);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        // Check if city exists in DB (used by the rest of CRUD methods)
        private bool CityExists(int id)
        {
            return (_context.Cities?.Any(e => e.Id == id)).GetValueOrDefault();
        }


        // Map [City => CityDto]
        private static CityDto ItemToDTO(City city) =>
            new CityDto
            {
                Id = city.Id,
                Name = city.Name,
                Population = city.Population,
                IsCapital = city.IsCapital
            };
    
    }
}

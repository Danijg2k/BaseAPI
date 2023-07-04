using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BaseAPI.Context;
using BaseAPI.Models;

namespace BaseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<City>>> GetCities()
        {
            return await _context.Cities.ToListAsync();
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
        [HttpGet("{id}")]
        public async Task<ActionResult<City>> GetCity(int id)
        {
            var city = await _context.Cities.FindAsync(id);

            if (city == null)
            {
                return NotFound();
            }

            return city;
        }

        // PUT: api/Cities/5

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="id">id of the [city] to update</param>
        /// <param name="city">new data for the updated [city]</param>
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
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCity(int id, City city)
        {
            if (id != city.Id)
            {
                return BadRequest();
            }

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
        /// <param name="city">data of the [city] to add</param>
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
        [HttpPost]
        public async Task<ActionResult<City>> PostCity(City city)
        {
          if (_context.Cities == null)
          {
              return Problem("Entity set 'CityContext.Cities'  is null.");
          }
            _context.Cities.Add(city);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCity", new { id = city.Id }, city);
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
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCity(int id)
        {
            if (_context.Cities == null)
            {
                return NotFound();
            }
            var city = await _context.Cities.FindAsync(id);
            if (city == null)
            {
                return NotFound();
            }

            _context.Cities.Remove(city);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CityExists(int id)
        {
            return (_context.Cities?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

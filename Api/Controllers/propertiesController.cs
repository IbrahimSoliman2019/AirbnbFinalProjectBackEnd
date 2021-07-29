using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Infrastructure.Data;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class propertiesController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public propertiesController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/properties
        [HttpGet]
        public async Task<ActionResult<IEnumerable<property>>> GetProperties()
        {
            return await _context.Properties.ToListAsync();
        }

        // GET: api/properties/5
        [HttpGet("{id}")]
        public async Task<ActionResult<property>> Getproperty(int id)
        {
            var @property = await _context.Properties.FindAsync(id);

            if (@property == null)
            {
                return NotFound();
            }

            return @property;
        }

        // PUT: api/properties/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putproperty(int id, property @property)
        {
            if (id != @property.id)
            {
                return BadRequest();
            }

            _context.Entry(@property).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!propertyExists(id))
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

        // POST: api/properties
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<property>> Postproperty(property @property)
        {
            _context.Properties.Add(@property);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getproperty", new { id = @property.id }, @property);
        }

        // DELETE: api/properties/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deleteproperty(int id)
        {
            var @property = await _context.Properties.FindAsync(id);
            if (@property == null)
            {
                return NotFound();
            }

            _context.Properties.Remove(@property);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool propertyExists(int id)
        {
            return _context.Properties.Any(e => e.id == id);
        }
    }
}

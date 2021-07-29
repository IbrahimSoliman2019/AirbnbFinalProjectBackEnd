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
    public class statesController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public statesController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/states
        [HttpGet]
        public async Task<ActionResult<IEnumerable<state>>> GetStates()
        {
            return await _context.States.ToListAsync();
        }

        // GET: api/states/5
        [HttpGet("{id}")]
        public async Task<ActionResult<state>> Getstate(int id)
        {
            var state = await _context.States.FindAsync(id);

            if (state == null)
            {
                return NotFound();
            }

            return state;
        }

        // PUT: api/states/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putstate(int id, state state)
        {
            if (id != state.id)
            {
                return BadRequest();
            }

            _context.Entry(state).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!stateExists(id))
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

        // POST: api/states
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<state>> Poststate(state state)
        {
            _context.States.Add(state);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getstate", new { id = state.id }, state);
        }

        // DELETE: api/states/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletestate(int id)
        {
            var state = await _context.States.FindAsync(id);
            if (state == null)
            {
                return NotFound();
            }

            _context.States.Remove(state);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool stateExists(int id)
        {
            return _context.States.Any(e => e.id == id);
        }
    }
}

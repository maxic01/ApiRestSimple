using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiRest.Data;
using ApiRest.Models;

namespace ApiRest.Controllers
{
    [Route("api/[controller]")] //URL/api/controller(controller = Libros) = api/Libros
    [ApiController]
    //SSL CERTIFICATE VERIFICATION OFF
    
    public class LibrosController : ControllerBase
    {
        private readonly ApiRestContext _context;

        public LibrosController(ApiRestContext context)
        {
            _context = context;
        }

        // GET: api/Libros
        [HttpGet] //SELECT, OBTENER
        public async Task<ActionResult<IEnumerable<Libros>>> GetLibros()
        {
            return await _context.Libros.ToListAsync();
        }

        // GET: api/Libros/5
        [HttpGet("{id}")] //SELECT, OBTENER POR ID  
        public async Task<ActionResult<Libros>> GetLibros(int id)
        {
            var libros = await _context.Libros.FindAsync(id);

            if (libros == null)
            {
                return NotFound();
            }

            return libros;
        }

        // PUT: api/Libros/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")] //UPDATE POR ID
        public async Task<IActionResult> PutLibros(int id, Libros libros)
        {
            if (id != libros.Id)
            {
                return BadRequest();
            }

            _context.Entry(libros).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LibrosExists(id))
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

        // POST: api/Libros
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost] //INSERT
        public async Task<ActionResult<Libros>> PostLibros(Libros libros)
        {
            _context.Libros.Add(libros);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLibros", new { id = libros.Id }, libros);
        }

        // DELETE: api/Libros/5
        [HttpDelete("{id}")] //DELETE POR ID
        public async Task<IActionResult> DeleteLibros(int id)
        {
            var libros = await _context.Libros.FindAsync(id);
            if (libros == null)
            {
                return NotFound();
            }

            _context.Libros.Remove(libros);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LibrosExists(int id)
        {
            return _context.Libros.Any(e => e.Id == id);
        }
        
        //POSTMAN
        //HTTP RESPONSE DE TIPO 200 = REQUEST SUCCESSFULL
        //HTTP RESPONSE DE TIPO 201 = REQUEST FULLFILLED
        //HTTP RESPONSE DE TIPO 204 = SUCCESSFULLY PROCESSED REQUEST
    }
}

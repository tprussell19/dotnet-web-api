using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectName.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace ProjectName.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ObjectsController : ControllerBase
  {
    private readonly ProjectNameContext _db;

    public ObjectsController(ProjectNameContext db)
    {
      _db = db;
    }

    private bool ObjectExists(int id) => _db.Objects.Any(a => a.ObjectId == id);

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Object>>> Get()
    => await _db.Objects.ToListAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<Object>> GetObject(int id)
    {
      var object = await _db.Objects.FindAsync(id);
      if (object == null) return NotFound();
      return review;
    }

    [HttpPost]
    public async Task<ActionResult<Object>> Post(Object r)
    {
      _db.Objects.Add(r);
      await _db.SaveChangesAsync();
      return CreatedAtAction(nameof(GetObject), new { id = r.ObjectId }, r);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Object r)
    {
      if (id != r.ObjectId) return BadRequest();

      _db.Entry(r).State = EntityState.Modified;

      try
      {
        await _db.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!ObjectExists(id)) return NotFound();
        else throw;
      }
      return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteObject(int id)
    {
      Object r = await _db.Objects.FindAsync(id);
      if (r == null) return NotFound();

      _db.Objects.Remove(r);
      await _db.SaveChangesAsync();

      return NoContent();
    }
  }
}
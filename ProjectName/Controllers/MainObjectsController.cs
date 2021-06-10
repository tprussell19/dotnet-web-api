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
  [ApiVersion("1.0")]
  [Route("api/v{version:ApiVersion}/[controller]")]
  public class MainObjectsController : ControllerBase
  {
    private readonly ProjectNameContext _db;

    public MainObjectsController(ProjectNameContext db)
    {
      _db = db;
    }

    private bool MainObjectExists(int id) => _db.MainObjects.Any(a => a.MainObjectId == id);

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MainObject>>> Get()
    => await _db.MainObjects.ToListAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<MainObject>> GetMainObject(int id)
    {
      var o = await _db.MainObjects.FindAsync(id);
      if (o == null) return NotFound();
      return o;
    }

    [HttpPost]
    public async Task<ActionResult<MainObject>> Post(MainObject r)
    {
      _db.MainObjects.Add(r);
      await _db.SaveChangesAsync();
      return CreatedAtAction(nameof(GetMainObject), new { id = r.MainObjectId }, r);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, MainObject r)
    {
      if (id != r.MainObjectId) return BadRequest();

      _db.Entry(r).State = EntityState.Modified;

      try
      {
        await _db.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!MainObjectExists(id)) return NotFound();
        else throw;
      }
      return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMainObject(int id)
    {
      MainObject r = await _db.MainObjects.FindAsync(id);
      if (r == null) return NotFound();

      _db.MainObjects.Remove(r);
      await _db.SaveChangesAsync();

      return NoContent();
    }
  }
}
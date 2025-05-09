using Microsoft.AspNetCore.Mvc;
using KeyManagementAPI.Data;
using KeyManagementAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace KeyManagementAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class KeysController : ControllerBase
{
    private readonly AppDbContext _context;

    public KeysController(AppDbContext context) => _context = context;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Key key)
    {
        key.Id = Guid.NewGuid();
        key.CreatedOn = DateTime.UtcNow;

        _context.Keys.Add(key);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(Get), new { id = key.Id }, key);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var keys = await _context.Keys.ToListAsync();
        return Ok(keys);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var key = await _context.Keys.FindAsync(id);
        if (key == null) return NotFound();
        return Ok(key);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var key = await _context.Keys.FindAsync(id);
        if (key == null) return NotFound();
        key.Status = KeyStatus.Deleted;

        await _context.SaveChangesAsync();
        return Ok(new { message = "Successfully deleted key" });
    }

    [HttpPost("{id}/encrypt")]
    public async Task<IActionResult> Encrypt(Guid id, [FromBody] byte[] pt)
    {
        return Ok();
    }

    [HttpPost("{id}/decrypt")]
    public async Task<IActionResult> Decrypt(Guid id, [FromBody] byte[] ct)
    {
        return Ok();
    }
}

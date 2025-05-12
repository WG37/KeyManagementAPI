using Microsoft.AspNetCore.Mvc;
using KeyManagementAPI.Data;
using KeyManagementAPI.Entities;
using KeyManagementAPI.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace KeyManagementAPI.Controllers;

[ApiController]
[Route("api/keys")]
public class KeysController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;

    public KeysController(AppDbContext db, IMapper mapper)  => 
        
        (_db, _mapper) = (db, mapper); 


    [HttpPost]
    public async Task<ActionResult<KeyDto>> Create([FromBody] CreateKeyDto keyDto)
    {
        var key = _mapper.Map<Key>(keyDto);        // auto maps CreateKeyDto => Key

        key.Id = Guid.NewGuid();
        key.CreatedOn = DateTime.UtcNow;

        var bytes = new byte[key.KeySize / 8];
        RandomNumberGenerator.Create().GetBytes(bytes);
        key.Keybytes = bytes;

        _db.Keys.Add(key);
        await _db.SaveChangesAsync();

        var result = _mapper.Map<KeyDto>(key);          // Key => keyDto
       
        return CreatedAtAction(nameof(Get), new { id = key.Id }, result);
    }

    [HttpGet]
    public async Task<ActionResult<KeyDto>> GetAll()
    {
        var keys = await _db.Keys.ProjectTo<KeyDto>(_mapper.ConfigurationProvider).ToListAsync();
        return Ok(keys);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<KeyDto>> Get(Guid id)
    {
        var key = await _db.Keys.FindAsync(id);
        if (key == null) 
            return NotFound(); 

        if (key.Status != KeyStatus.Active) 
        return BadRequest(new { message = "Key is not active or does not exist" });

        var dto = _mapper.Map<KeyDto>(key);

        return Ok(dto);
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var key = await _db.Keys.FindAsync(id);
        if (key == null) return NotFound();

        key.Status = KeyStatus.Deleted;
        await _db.SaveChangesAsync();
        return NoContent();
    }
}

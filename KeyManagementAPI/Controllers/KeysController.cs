using Microsoft.AspNetCore.Mvc;
using KeyManagementAPI.Data;
using KeyManagementAPI.Entities;
using KeyManagementAPI.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using KeyManagementAPI.Services.Symmetric.KeyServices;

namespace KeyManagementAPI.Controllers;

[ApiController]
[Route("api/keys")]
public class KeysController : ControllerBase
{
    private readonly IKeyService _service;

    public KeysController(IKeyService service)  => _service = service; 


    [HttpPost]
    public async Task<ActionResult<KeyDto>> Create([FromBody] CreateKeyDto keyDto)
    {
        var result = await _service.CreateAsync(keyDto);
        return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
    }

    [HttpGet]
    public async Task<ActionResult<List<KeyDto>>> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<KeyDto>> Get(Guid id)
    {
        return Ok(await _service.GetAsync(id));
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}

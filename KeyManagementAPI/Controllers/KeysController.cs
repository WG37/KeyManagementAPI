using Microsoft.AspNetCore.Mvc;
using KeyManagementAPI.Data;
using KeyManagementAPI.Entities;

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
        
       
    }
}

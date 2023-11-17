using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wpm.Web.Dal;

namespace Wpm.Web.Api;
[Route("api/[controller]")]
[ApiController]
public class PetsController : ControllerBase
{
    private readonly WpmDbContext dbContext;

    public PetsController(WpmDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IActionResult> Search(string query)
    {
        var result = await dbContext
                .Pets
                .FromSqlRaw($"SELECT * FROM Pets WHERE Name LIKE '%{query}%'")
                .ToListAsync();
        return Ok(result);
    }
}
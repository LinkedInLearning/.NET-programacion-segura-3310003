using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Wpm.Web.Dal;
using Wpm.Web.Domain;

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

    [HttpPost]
    public IActionResult Create(string payload)
    {
        var newPet = JsonConvert.DeserializeObject<Pet>(payload);

        // Save...

        return Ok();
    }
}
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;
using Wpm.Web.Dal;
using Wpm.Web.Domain;

namespace Wpm.Web.Api;
[Route("api/[controller]")]
[ApiController]
public class PetsController : ControllerBase
{
    private readonly WpmDbContext dbContext;
    private readonly IConfiguration configuration;

    public PetsController(WpmDbContext dbContext,
                          IConfiguration configuration)
    {
        this.dbContext = dbContext;
        this.configuration = configuration;
    }

    [HttpPost]
    public IActionResult Create(string payload, string signature)
    {
        var key = Encoding.ASCII.GetBytes(configuration["SecretKey"]);
        using var hmacsha256 = new HMACSHA256(key);
        var hash = hmacsha256.ComputeHash(Encoding.ASCII.GetBytes(payload));
        var computedSignature = Convert.ToBase64String(hash);

        if (computedSignature != signature)
        {
            return BadRequest("Invalid data integrity.");
        }

        try
        {
            var data = JsonConvert.DeserializeObject<Pet>(payload);
            
            //Save...
        }
        catch (JsonException)
        {
            return BadRequest("Deserialization error.");
        }
        return Ok();
    }
}
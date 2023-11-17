using Microsoft.AspNetCore.Mvc;

namespace Wpm.Web.Api;
[Route("api/[controller]")]
[ApiController]
public class PetsController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> ProcessPetPhoto(string photoUrl)
    {
        using var httpClient = new HttpClient();
        var imageBytes = await httpClient.GetByteArrayAsync(photoUrl);

        // Process...

        return Ok("Photo processed successfully.");
    }
}
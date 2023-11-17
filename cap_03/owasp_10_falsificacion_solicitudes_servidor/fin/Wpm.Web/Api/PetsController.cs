using Microsoft.AspNetCore.Mvc;

namespace Wpm.Web.Api;
[Route("api/[controller]")]
[ApiController]
public class PetsController : ControllerBase
{
    private readonly IHttpClientFactory httpClientFactory;

    public PetsController(IHttpClientFactory httpClientFactory)
    {
        this.httpClientFactory = httpClientFactory;
    }

    [HttpPost]
    public async Task<IActionResult> ProcessPetPhoto(string photoUrl)
    {
        if (!UrlValidator.IsPhotoUrlAllowed(photoUrl))
        {
            return BadRequest("Invalid URL.");
        }

        var httpClient = httpClientFactory.CreateClient("PetPhotoClient");
        byte[] imageBytes;
        try
        {
            imageBytes = await httpClient.GetByteArrayAsync(photoUrl);
        }
        catch (HttpRequestException)
        {
            return BadRequest("Photo error.");
        }

        // Process...

        return Ok("Photo processed successfully.");
    }
}

public static class UrlValidator
{
    private static readonly List<string> hosts = ["wisdompetmed.com", "unsplash.com"];

    public static bool IsPhotoUrlAllowed(string imageUrl)
    {
        if (!Uri.TryCreate(imageUrl, UriKind.Absolute, out var uriResult))
        {
            return false;
        }

        return hosts.Contains(uriResult.Host) && uriResult.Scheme == Uri.UriSchemeHttps;
    }
}
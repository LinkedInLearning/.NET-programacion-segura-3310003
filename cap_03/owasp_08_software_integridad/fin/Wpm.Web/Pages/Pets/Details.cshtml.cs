using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Wpm.Web.Dal;
using Wpm.Web.Domain;

namespace Wpm.Web.Pages.Pets;

public class DetailsModel : PageModel
{
    private readonly WpmDbContext dbContext;

    public Pet? Pet { get; set; }
    public DetailsModel(WpmDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public void OnGet(int? id)
    {
        var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier);
        if (userIdClaim != null)
        {
            var userId = userIdClaim.Value;
            // Use userId as needed
        }

        var pet = dbContext.Pets
            .Where(p => p.Id == id)
            .Include(p => p.Owners)
            .Include(p => p.Breed)
            .ThenInclude(b => b.Species)
            .First();

        Pet = pet;
    }
}
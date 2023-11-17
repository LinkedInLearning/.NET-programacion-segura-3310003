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
        Guid userId = Guid.Empty;
        var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier);
        if (userIdClaim != null)
        {
            userId = Guid.Parse(userIdClaim.Value);
        }

        var pet = dbContext.Pets
            .Where(p => p.Id == id && p.UserId == userId)
            .Include(p => p.Owners)
            .Include(p => p.Breed)
            .ThenInclude(b => b.Species)
            .FirstOrDefault();

        if (pet == null)
        {
            throw new UnauthorizedAccessException("Sin permiso");
        }

        Pet = pet;
    }
}
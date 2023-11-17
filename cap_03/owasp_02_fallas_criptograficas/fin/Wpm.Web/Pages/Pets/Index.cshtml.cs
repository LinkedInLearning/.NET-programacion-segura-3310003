using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Wpm.Web.Dal;
using Wpm.Web.Domain;

namespace Wpm.Web.Pages.Pets;

public class IndexModel : PageModel
{
    private readonly WpmDbContext dbContext;

    public IEnumerable<Pet>? Pets { get; private set; }

    [BindProperty(SupportsGet = true)]
    public string? Search { get; set; }

    public IndexModel(WpmDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public void OnGet()
    {
        Pets = dbContext.Pets
            .Include(p => p.Breed)
            .ThenInclude(b => b.Species)
            .Where(p => string.IsNullOrWhiteSpace(Search) ? true :
                    p.Name.ToLowerInvariant().Contains(Search))
            .ToList();
    }
}
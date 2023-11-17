using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Wpm.Web.Dal;
using Wpm.Web.Domain;

namespace Wpm.Web.Pages.Pets;

public class EditModel : PageModel
{
    private readonly WpmDbContext dbContext;

    [BindProperty]
    public Pet? Pet { get; set; }

    public SelectList? Breeds { get; set; }

    public EditModel(WpmDbContext dbContext)
    {
        this.dbContext = dbContext;

        var breeds = dbContext
            .Breeds
            .Select(b => new SelectListItem(b.Name, b.Id.ToString())).ToList();
        Breeds = new SelectList(breeds, "Value", "Text");
    }
    public void OnGet(int id)
    {
        Pet = dbContext.Pets
                    .Where(p => p.Id == id)
                    .First();
    }

    public async Task<IActionResult> OnPost()
    {
        dbContext.Update(Pet);
        await dbContext.SaveChangesAsync();
        return RedirectToPage("Index");
    }
}
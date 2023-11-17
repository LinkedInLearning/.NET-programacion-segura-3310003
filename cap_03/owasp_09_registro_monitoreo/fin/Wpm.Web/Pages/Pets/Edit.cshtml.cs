using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Wpm.Web.Dal;
using Wpm.Web.Domain;
using Wpm.Web.Services;

namespace Wpm.Web.Pages.Pets;

public class EditModel : PageModel
{
    private readonly WpmDbContext dbContext;
    private readonly EncryptionService encryptionService;

    [BindProperty]
    public Pet? Pet { get; set; }

    [BindProperty]
    public string? MicrochipNumber { get; set; }

    public SelectList? Breeds { get; set; }

    public EditModel(WpmDbContext dbContext, EncryptionService encryptionService)
    {
        this.dbContext = dbContext;
        this.encryptionService = encryptionService;

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
        if (Pet.MicrochipNumber != null)
        {
            MicrochipNumber = encryptionService.DecryptData(Pet.MicrochipNumber);
        }
    }

    public async Task<IActionResult> OnPost()
    {
        var encryptedMicrochipNumber = encryptionService.EncryptData(MicrochipNumber);
        Pet.MicrochipNumber = encryptedMicrochipNumber;

        dbContext.Update(Pet);
        await dbContext.SaveChangesAsync();
        return RedirectToPage("Index");
    }
}
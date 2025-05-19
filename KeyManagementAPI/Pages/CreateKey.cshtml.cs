using KeyManagementAPI.DTOs;
using KeyManagementAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace KeyManagementAPI.Pages
{
    public class CreateKeyModel : PageModel
    {
        private readonly IKeyService _keyService;
        public CreateKeyModel(IKeyService keyservice) => _keyService = keyservice;

        [BindProperty]
        public CreateKeyDto Input { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            await _keyService.CreateAsync(Input);
            return RedirectToPage("Index");
        }
    }
}

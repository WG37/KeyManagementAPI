using KeyManagementAPI.DTOs;
using KeyManagementAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace KeyManagementAPI.Pages
{
    public class CreateKeyModel : PageModel
    {
        private readonly IKeyService _keyservice;
        public CreateKeyModel(IKeyService keyservice) => _keyservice = keyservice;

        [BindProperty]
        public CreateKeyDto Input { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            await _keyservice.CreateAsync(Input);
            return RedirectToPage("Index");
        }
    }
}

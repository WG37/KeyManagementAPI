using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Json;

namespace KeyManagementAPI.Pages
{
    public class CreateKeyModel : PageModel
    {
        private readonly IHttpClientFactory _factory;
        public CreateKeyModel(IHttpClientFactory factory) => _factory = factory;

        [BindProperty]
        public string Name { get; set; }

        [BindProperty]
        public int KeySize { get; set; } = 256;

        public async Task<IActionResult> OnPostAsync()
        {
            var client = _factory.CreateClient("Api");
            var response = await client.PostAsJsonAsync("Api/Keys", new { Name, KeySize });

            if (response.IsSuccessStatusCode) 
                return RedirectToPage("Index");

            return Page();
        }
    }
}

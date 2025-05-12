using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using KeyManagementAPI.DTOs;


namespace KeyManagementAPI.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _factory;
        public IndexModel(IHttpClientFactory factory) => _factory = factory;

        public List<KeyDto> Keys { get; set; }

        public async Task OnGetAsync()
        {
            var client = _factory.CreateClient("Api");
            Keys = await client.GetFromJsonAsync<List<KeyDto>>("api/Keys");
        }

        public async Task<IActionResult> OnPostDeleteAsync(Guid id)
        {
            var client = _factory.CreateClient("Api");
            await client.DeleteAsync($"api/keys/{id}");
            return RedirectToPage();
        }
    }
}

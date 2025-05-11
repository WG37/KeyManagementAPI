using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.RazorPages;
using KeyManagementAPI.Entities;


namespace KeyManagementAPI.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _factory;

        public List<Key> Keys { get; set; }

        public IndexModel(IHttpClientFactory factory) => _factory = factory;

        public async Task OnGetAsync()
        {
            var client = _factory.CreateClient("Api");
            Keys = await client.GetFromJsonAsync<List<Key>>("Api/keys");
        }
    }
}

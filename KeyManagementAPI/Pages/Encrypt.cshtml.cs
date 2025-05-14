using KeyManagementAPI.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KeyManagementAPI.Pages
{
    public class EncryptModel : PageModel
    {
        private readonly IHttpClientFactory _factory;
        public EncryptModel(IHttpClientFactory factory) => _factory = factory;


        [BindProperty]
        public Guid? SelectedKeyId { get; set; }
        [BindProperty]
        public string PlainText { get; set; }

        public List<KeyDto> Keys { get; private set; }
        public EncryptResponse Result { get; private set; }

        public async Task OnGetAsync()
        {
            Keys = await _factory.CreateClient("Api").GetFromJsonAsync<List<KeyDto>>("api/keys");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await OnGetAsync();
            var client = _factory.CreateClient("Api");
            var response = await client.PostAsJsonAsync($"api/keys/{SelectedKeyId}/encrypt", new EncryptRequest { PlainText = PlainText });

            if (response.IsSuccessStatusCode)
            {
                Result = await response.Content.ReadFromJsonAsync<EncryptResponse>();
            }
            return Page();
        }
    }
}

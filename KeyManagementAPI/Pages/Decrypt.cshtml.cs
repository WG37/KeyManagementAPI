using KeyManagementAPI.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KeyManagementAPI.Pages
{
    public class DecryptModel : PageModel
    {
        private readonly IHttpClientFactory _factory;
        public DecryptModel(IHttpClientFactory factory) => _factory = factory;

        [BindProperty]
        public Guid? SelectedKeyId { get; set; }
        [BindProperty]
        public string CipherText { get; set; }
        [BindProperty]
        public string Iv { get; set; }

        public List<KeyDto> Keys { get; private set; }
        public DecryptResponse Result { get; private set; }

        public async Task OnGetAsync()
        {
            Keys = await _factory.CreateClient("Api").GetFromJsonAsync<List<KeyDto>>("api/keys");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!SelectedKeyId.HasValue)
            {
                ModelState.AddModelError(nameof(SelectedKeyId), "A key must be selected");

                Keys = await _factory.CreateClient("Api").GetFromJsonAsync<List<KeyDto>>("api/keys");
                return Page();
            }


            await OnGetAsync();
            var client = _factory.CreateClient("Api");
            var response = await client.PostAsJsonAsync($"api/keys/{SelectedKeyId}/decrypt", new DecryptRequest { CipherText = CipherText, Iv = Iv });

            if (response.IsSuccessStatusCode)
            {
                Result = await response.Content.ReadFromJsonAsync<DecryptResponse>();
            }
            return Page();
        }
    }
}

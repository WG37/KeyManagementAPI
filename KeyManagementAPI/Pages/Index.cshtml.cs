using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using KeyManagementAPI.DTOs;
using KeyManagementAPI.Services;


namespace KeyManagementAPI.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IKeyService _keyservice;
        public IndexModel(IKeyService keyservice) => _keyservice = keyservice;

        public List<KeyDto> Keys { get; private set; } = new();

        public async Task OnGetAsync()
        {
            Keys = await _keyservice.GetAllAsync();
        }

        public async Task<IActionResult> OnPostDelete(Guid id)
        {
            await _keyservice.DeleteAsync(id);
            return RedirectToPage();
        }
    }
}

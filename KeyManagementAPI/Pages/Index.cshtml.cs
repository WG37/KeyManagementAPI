using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using KeyManagementAPI.DTOs;
using KeyManagementAPI.Services;


namespace KeyManagementAPI.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IKeyService _keyService;
        public IndexModel(IKeyService keyservice) => _keyService = keyservice;

        public List<KeyDto> Keys { get; private set; } = new();

        public async Task OnGetAsync()
        {
            Keys = await _keyService.GetAllAsync();
        }

        public async Task<IActionResult> OnPostDelete(Guid id)
        {
            await _keyService.DeleteAsync(id);
            return RedirectToPage();
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using KeyManagementAPI.Services;
using KeyManagementAPI.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace KeyManagementAPI.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly IKeyService _keyService;
        public IndexModel(IKeyService keyService) => _keyService = keyService;

      
        public List<KeyDto> Keys { get; private set; } = new List<KeyDto>();

        public async Task OnGetAsync()
        {
            Keys = await _keyService.GetAllAsync();
        }
    }
}

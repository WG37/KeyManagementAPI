using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using KeyManagementAPI.DTOs;
using KeyManagementAPI.Services;  // IKeyService & ICryptoService

namespace KeyManagementAPI.Pages
{
    public class DecryptModel : PageModel
    {
        private readonly IKeyService _keyService;
        private readonly ICipherService _cipherService;

        public DecryptModel(IKeyService keyService, ICipherService cryptoService)
        {
            _keyService = keyService;
            _cipherService = cryptoService;
        }

        public List<KeyDto> Keys { get; private set; } = new();
        public DecryptResponse Result { get; private set; }

        [BindProperty]
        public Guid SelectedKeyId { get; set; }

        [BindProperty]
        public string CipherText { get; set; }

        [BindProperty]
        public string Iv { get; set; }

        public async Task OnGetAsync()
        {
            Keys = await _keyService.GetAllAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // repopulate the key list for redisplay
            Keys = await _keyService.GetAllAsync();

            if (!ModelState.IsValid || SelectedKeyId == default)
            {
                ModelState.AddModelError("", "Please select a key and supply both CipherText and IV.");
                return Page();
            }

            // perform decryption
            Result = await _cipherService.DecryptAsync(
                SelectedKeyId,
                CipherText,
                Iv
            );
            return Page();
        }
    }
}

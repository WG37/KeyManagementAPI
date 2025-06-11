using KeyManagementAPI.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using KeyManagementAPI.DTOs.Symmetric;
using KeyManagementAPI.Services.SymmetricServices.KeyServices;
using KeyManagementAPI.Services.SymmetricServices.CipherServices;

namespace KeyManagementAPI.Pages.Encrypt.Symmetric
{
    public class EncryptModel : PageModel
    {
        private readonly IKeyService _keyService;
        private readonly ICipherService _cipherService;

        public EncryptModel(IKeyService keyService, ICipherService cryptoService)
        {
            _keyService = keyService;
            _cipherService = cryptoService;
        }

        public List<KeyDto> Keys { get; private set; } = new();
        public EncryptResponse Result { get; private set; }

        [BindProperty]
        public Guid SelectedKeyId { get; set; }
        [BindProperty]
        public string PlainText { get; set; }

        public async Task OnGetAsync()
        {
            Keys = await _keyService.GetAllAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // get all keys for dropdown
            Keys = await _keyService.GetAllAsync();

            if (!ModelState.IsValid || SelectedKeyId == Guid.Empty)
            {
                ModelState.AddModelError("", "Please select a key and/or enter plaintext.");
                return Page();
            }

            // perform encryption
            Result = await _cipherService.EncryptAsync(
                SelectedKeyId,
                PlainText
            );

            return Page();
        }
    }
}

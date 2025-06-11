using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using KeyManagementAPI.DTOs;
using KeyManagementAPI.DTOs.Symmetric;
using KeyManagementAPI.Services.SymmetricServices.KeyServices;
using KeyManagementAPI.Services.SymmetricServices.CipherServices;

namespace KeyManagementAPI.Pages.Decrypt.Symmetric
{
    public class DecryptModel : PageModel
    {
        private readonly IKeyService _keyService;
        private readonly ICipherService _cipherService;

        public DecryptModel(IKeyService keyService, ICipherService cipherService)
        {
            _keyService = keyService;
            _cipherService = cipherService;
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

            if (!ModelState.IsValid || SelectedKeyId == Guid.Empty)
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

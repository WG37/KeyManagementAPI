using KeyManagementAPI.DTOs;
using KeyManagementAPI.DTOs.Asymmetric;
using KeyManagementAPI.Services.AsymmetricServices.CipherServices;
using KeyManagementAPI.Services.AsymmetricServices.KeyServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KeyManagementAPI.Pages.Decrypt.Asymmetric
{
    public class DecryptModel : PageModel
    {
        private readonly IAsymCipherService _cipherService;
        private readonly IAsymKeyService _keyService;

        public DecryptModel(IAsymCipherService cipherService, IAsymKeyService keyService)
        {
            _cipherService = cipherService;
            _keyService = keyService;
        }

        public List<KeyDto> Keys { get; private set; } = new();
        public RsaDecryptResponse Result { get; private set; }

        [BindProperty]
        public Guid SelectedKeyId { get; set; }
        [BindProperty]
        public string CipherText { get; set; }

        public async Task OnGet()
        {
            Keys = await _keyService.AsymGetAllAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // repopulate dropdown
            Keys = await _keyService.AsymGetAllAsync();

            if (!ModelState.IsValid || SelectedKeyId == Guid.Empty)
            {
                ModelState.AddModelError("", "Please select a key and/or ciphertext.");
                return Page();
            }
            // decrypt
            Result = await _cipherService.DecryptAsync(
                SelectedKeyId,
                CipherText
            );
            return Page();
        }
    }
}

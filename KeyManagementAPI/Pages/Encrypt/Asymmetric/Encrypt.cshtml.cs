using KeyManagementAPI.DTOs;
using KeyManagementAPI.DTOs.Asymmetric;
using KeyManagementAPI.Services.AsymmetricServices.CipherServices;
using KeyManagementAPI.Services.AsymmetricServices.KeyServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KeyManagementAPI.Pages.Encrypt.Asymmetric
{
    public class EncryptModel : PageModel
    {
        private readonly IAsymCipherService _cipherService;
        private readonly IAsymKeyService _keyService;

        public EncryptModel(IAsymCipherService cipherService, IAsymKeyService keyService)
        {
            _cipherService = cipherService;
            _keyService = keyService;
        }

        public List<KeyDto> Keys { get; private set; } = new();
        public RsaEncryptResponse Result { get; private set; }

        [BindProperty]
        public Guid SelectedKeyId { get; set; }
        [BindProperty]
        public string PlainText { get; set; }


        public async Task OnGetAsync()
        {
            Keys = await _keyService.AsymGetAllAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // repopoulate dropdown
            Keys = await _keyService.AsymGetAllAsync();

            if (!ModelState.IsValid || SelectedKeyId == Guid.Empty)
            {
                ModelState.AddModelError("", "Please select a key and/or enter a plaintext.");

                return Page();
            }

            Result = await _cipherService.EncryptAsync(
                SelectedKeyId,
                PlainText
            );

            return Page();
        }

    }
}

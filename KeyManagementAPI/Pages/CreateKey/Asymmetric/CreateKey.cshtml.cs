using System.ComponentModel.DataAnnotations;
using KeyManagementAPI.DTOs;
using KeyManagementAPI.Services.AsymmetricServices.KeyServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KeyManagementAPI.Pages.CreateKey.Asymmetric
{
    public class CreateKeyModel : PageModel
    {
        private readonly IAsymKeyService _keyService;
        public CreateKeyModel(IAsymKeyService keyService) => _keyService = keyService;

        public class InputPreset
        {
            [Required, MinLength(3)]
            public string Name { get; set; }
            
            [Required] 
            public string Algorithm { get; set; } = "RSA";

            [Required]
            [RegularExpression ("2048|4096", ErrorMessage = "Key size must be 2048 or 4096-bits.")]
            public int KeySize { get; set; } = 2048;
        }

        [BindProperty]
        public InputPreset Input { get; set; } = new InputPreset();

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var keyDto = new CreateKeyDto
            {
                Name = Input.Name,
                KeySize = Input.KeySize,
                Algorithm = Input.Algorithm
            };

            await _keyService.AsymCreateAsync(keyDto);
            return RedirectToPage("/Index");
        }
    }
}

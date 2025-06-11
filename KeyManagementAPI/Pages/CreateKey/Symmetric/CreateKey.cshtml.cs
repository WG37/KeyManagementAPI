using KeyManagementAPI.DTOs;
using KeyManagementAPI.Services.SymmetricServices.KeyServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;


namespace KeyManagementAPI.Pages.CreateKey.Symmetric
{
    public class CreateKeyModel : PageModel
    {
        private readonly IKeyService _keyService;
        public CreateKeyModel(IKeyService keyservice) => _keyService = keyservice;

        public class InputPreset
        {
            [Required, MinLength(3)]
            public string Name { get; set; }

            public string Algorithm { get; set; } = "AES";

            [Required, Range(128, 256)]
            public int KeySize { get; set; } = 256;
        }


        [BindProperty]
        public InputPreset Input { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var keyDto = new CreateKeyDto
            {
                Name = Input.Name,
                KeySize = Input.KeySize,
                Algorithm = Input.Algorithm
            };

            await _keyService.CreateAsync(keyDto);
            return RedirectToPage("Index");
        }
    }
}

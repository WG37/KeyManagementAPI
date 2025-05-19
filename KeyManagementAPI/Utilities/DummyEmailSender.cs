using Microsoft.AspNetCore.Identity.UI.Services;

namespace KeyManagementAPI.Utilities
{
    public sealed class DummyEmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
            => Task.CompletedTask;
    }
}

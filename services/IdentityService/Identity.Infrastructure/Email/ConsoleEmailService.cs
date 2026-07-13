using Identity.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace Identity.Infrastructure.Email;

public sealed class ConsoleEmailService(
    ILogger<ConsoleEmailService> logger)
    : IEmailService
{
    public Task SendEmailAsync(
        string to,
        string subject,
        string body,
        CancellationToken cancellationToken = default)
    {
        logger.LogInformation(
            """

            ================= EMAIL =================
            To      : {To}
            Subject : {Subject}

            {Body}

            =========================================

            """,
            to,
            subject,
            body);

        return Task.CompletedTask;
    }
}
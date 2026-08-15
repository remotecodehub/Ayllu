using Ayllu.Application.Common.Abstractions.Culture;
using Ayllu.Application.Common.Abstractions.Email;
using Ayllu.Domain.Entities.Identity;
using Ayllu.Infrastructure.Communication.Email.Templates.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

using System.Net;
using System.Net.Mail;

namespace Ayllu.Infrastructure.Communication.Email.Smtp;

public sealed class GoogleSmtpEmailSender(
    IOptions<GoogleSmtpOptions> options,
    ICultureHelper cultureHelper,
    IEmailTemplateRenderer renderer) :
    IEmailSender<ApplicationUser>
{
    private readonly GoogleSmtpOptions _options = options.Value;
    private readonly string selectedCulture = cultureHelper.GetCurrentCulture();
    public async Task SendConfirmationLinkAsync(
        ApplicationUser user,
        string email,
        string confirmationLink)
    => await PrepareToSend("ConfirmEmail", "Link de confirmação de e-mail", email , new ConfirmEmailModel
            {
                UserName = user.Email!,
                ConfirmationLink = confirmationLink
            }, default!);

    public async Task SendPasswordResetLinkAsync(
        ApplicationUser user,
        string email,
        string resetLink)
        => await PrepareToSend("PasswordResetLinkEmail", "Link de redefinição de senha", email , new PasswordResetLinkEmailModel
            {
                UserName = user.Email!,
                ResetLink = resetLink
            }, default!);
    
    public async Task SendPasswordResetCodeAsync(
        ApplicationUser user,
        string email,
        string resetCode)
        => await PrepareToSend("PasswordResetCodeEmail", "Código de recuperação da senha", email , new PasswordResetCodeEmailModel
            {
                UserName = user.Email!,
                ResetCode = resetCode
            }, default!);

    private async Task PrepareToSend(string template, string subject, string email, object model, CancellationToken cancellationToken = default )
    {
        var body = await renderer.RenderAsync(
            template,
            model, 
            selectedCulture,
            cancellationToken);

        await SendAsync(
            email,
            subject,
            body);
    }

    private async Task SendAsync(
        string to,
        string subject,
        string body)
    {
        using var client = new SmtpClient(_options.Host, _options.Port)
        {
            Credentials = new NetworkCredential(
                _options.Username,
                _options.Password),
            EnableSsl = _options.UseSsl
        };

        var message = new MailMessage()
        {
            From = new MailAddress(
                _options.FromEmail,
                _options.FromName),
            Subject = subject,
            Body = body,
            IsBodyHtml = true
        };

        message.To.Add(to);

        await client.SendMailAsync(message);
    }
}
using System.Net.Mail;
using MediatR;
using Microsoft.Extensions.Configuration;
using SeCumple.Application.Components.Notification.Dtos;
using SeCumple.Application.Dtos.Response;

namespace SeCumple.Application.Components.Notification.Commands;

public class SendEmailCommandHandler(IConfiguration configuration)
    : IRequestHandler<SendEmailCommand, ProcessResult<EmailResponse>>
{
    public Task<ProcessResult<EmailResponse>> Handle(SendEmailCommand request, CancellationToken cancellationToken)
    {
        var email = new MailMessage
        {
            From = new MailAddress(configuration["Settings:cEmail"]!),  
            Subject = request.Subject,
            Body = request.Body,
            Priority = MailPriority.High,
            IsBodyHtml = true
        };
        foreach (var item in request.To!)
        {
            if (!string.IsNullOrWhiteSpace(item)) 
                email.To.Add(item);
        }

        foreach (var item in request.CC!)
        {
            if (!string.IsNullOrWhiteSpace(item))
                email.CC.Add(item);
        }

        var smtp = new SmtpClient(configuration["Settings:cServidor"]);
        smtp.Port = Convert.ToInt32(configuration["Settings:cSPuertoSMTP"]);

        var responseEmail = new ProcessResult<EmailResponse>();

        try
        {
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential(configuration["Settings:cEmail"],
                configuration["Settings:cPassword"]);
            smtp.EnableSsl = true;
            smtp.Send(email);
            email.Dispose();
            responseEmail.Data = new EmailResponse
            {
                Message = "Email Sent"
            };
        }
        catch (Exception e)
        {
            email.Dispose();
            responseEmail.IsSuccess = false;
            responseEmail.Message = new[] { e.Message };
            responseEmail.Data = new EmailResponse
            {
                Message = "Email Sent"
            };
        }

        return Task.FromResult(responseEmail);
    }
}

public class SendEmailCommand : IRequest<ProcessResult<EmailResponse>>
{
    public string[]? To { get; set; }
    public string[]? CC { get; set; }
    public string? Subject { get; set; }
    public string? Body { get; set; }
}
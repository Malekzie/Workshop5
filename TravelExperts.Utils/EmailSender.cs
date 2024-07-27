namespace TravelExperts.Utils
{

    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            //TODO: Implement email sender
            return Task.CompletedTask;
        }
    }
}

namespace Delivery.Application.Settings;

public class EmailSenderSettings
{
    public const string SectionName = "EmailSenderSettings";

    public string ApiKey { get; set; }
    public string FromEmail { get; set; }
    public string FromName { get; set; }
}
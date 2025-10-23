using System.Text;

namespace Delivery.Domain.Entities.UserEntities;

public class Voucher
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public DateTime DateIssued { get; set; }
    public DateTime ExpirationDate { get; set; }
    public double DiscountAmount { get; set; }
    public string Status { get; set; } //Active, Used, Expired, Inactive
    public Guid CustomerId { get; set; }

    public Voucher()
    {
        Code = GenerateRandomCode(5);
        DateIssued = DateTime.UtcNow;
        ExpirationDate = DateIssued.AddHours(24);
        Status = "Active";
    }

    private static string GenerateRandomCode(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var random = new Random();

        var codeBuilder = new StringBuilder(length);

        for (int i = 0; i < length; i++)
        {
            codeBuilder.Append(chars[random.Next(chars.Length)]);
        }

        return codeBuilder.ToString();
    }
}

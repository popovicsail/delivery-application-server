using Delivery.Domain.Entities.OrderEntities;

namespace Delivery.Application.Interfaces;

public interface IPdfService
{
    byte[] GenerateBillPdf(Bill bill);
}

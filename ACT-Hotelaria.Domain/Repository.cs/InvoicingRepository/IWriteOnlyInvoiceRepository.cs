using ACT_Hotelaria.Domain.Entities;

namespace ACT_Hotelaria.Domain.Repository.cs.InvoicingRepository;

public interface IWriteOnlyInvoiceRepository
{
    Task Add(Domain.Entities.Invoicing invoice);
    
}
using ACT_Hotelaria.Domain.Entities;
using ACT_Hotelaria.Domain.Repository.InvoicingRepository;

namespace ACT_Hotelaria.SqlServer.Repository;

public class InvoicingRepository : IWriteOnlyInvoiceRepository
{
    private readonly ACT_HotelariaDbContext _context;
    
    public InvoicingRepository(ACT_HotelariaDbContext context)
    {
        _context = context;
    }
    
    public async Task Add(Domain.Entities.Invoicing invoice)
    {
        _context.Invoicings.Add(invoice);
    }
}
namespace ACT_Hotelaria.Application.UseCase.Invoicing;

public class RegisterInvoicingUseCaseResponse
{
    public Guid InvoiceId { get; set; }
    public decimal TotalRoom { get; set; }
    public decimal TotalConsumption { get; set; }
    public decimal GrandTotal{ get; set; }
    public DateTime IssueDate { get; set; }
}
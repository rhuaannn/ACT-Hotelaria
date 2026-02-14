using ACT_Hotelaria.Application.Abstract.Query;
using ACT_Hotelaria.Domain.Repository.ConsumptionRepository.cs;

namespace ACT_Hotelaria.Application.UseCase.Consumption.GetAll;

public class GetAllConsumptionUseCase :IQueryHandler<GetAllQueryConsumption, IEnumerable<GetAllConsumptionUseCaseResponse>>
{
    private readonly IReadOnlyConsumptionRepository _readOnlyConsumptionRepository; 
    
    public GetAllConsumptionUseCase(IReadOnlyConsumptionRepository readOnlyConsumptionRepository)
    {
        _readOnlyConsumptionRepository = readOnlyConsumptionRepository;
    }
         
    public async Task<IEnumerable<GetAllConsumptionUseCaseResponse>> Handle(GetAllQueryConsumption query, CancellationToken cancellationToken)
    {
        var consumptions = await _readOnlyConsumptionRepository.GetAllAsync();

        var response = consumptions.Select(consumption => new GetAllConsumptionUseCaseResponse
        {
            Id = consumption.Id,
            ProductId = consumption.ProductId,
            Quantity = consumption.QtyProduct,
            Value = consumption.TotalValue
        });
    
        return response;
    }
}
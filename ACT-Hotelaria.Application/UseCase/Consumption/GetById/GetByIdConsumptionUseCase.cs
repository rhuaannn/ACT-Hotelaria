using ACT_Hotelaria.Application.Abstract.Query;
using ACT_Hotelaria.Domain.Exception;
using ACT_Hotelaria.Domain.Repository.ConsumptionRepository.cs;

namespace ACT_Hotelaria.Application.UseCase.Consumption.GetById;

public class GetByIdConsumptionUseCase : IQueryHandler<GetByIdQueryConsumption, GetByIdConsumptionUseCaseResponse>
{
    private readonly IReadOnlyConsumptionRepository _readOnlyConsumptionRepository;
    
    public async Task<GetByIdConsumptionUseCaseResponse> Handle(GetByIdQueryConsumption query, CancellationToken cancellationToken)
    {
        var consumption = await _readOnlyConsumptionRepository.GetByIdAsync(query.Id);
        if (consumption == null)
        {
            throw new DomainException("Consumo não encontrado");
        }
        var response = new GetByIdConsumptionUseCaseResponse
        {
            Id = consumption.Id,
            Quantity = consumption.QtyProduct,
            Value = consumption.TotalValue
        };
        return response;
    }
}
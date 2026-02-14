using ACT_Hotelaria.Application.Abstract.Query;

namespace ACT_Hotelaria.Application.UseCase.Consumption.GetById;

public class GetByIdQueryConsumption : IQuery<GetByIdConsumptionUseCaseResponse>
{
    public Guid Id { get; set; }

    public GetByIdQueryConsumption(Guid id) => Id = id;
}
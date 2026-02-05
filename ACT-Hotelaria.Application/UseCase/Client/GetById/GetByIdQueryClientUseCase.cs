using ACT_Hotelaria.Application.Abstract.Query;

namespace ACT_Hotelaria.Application.UseCase.Client.GetById;

public class GetByIdQueryClientUseCase : IQuery<GetByIdClientUseCaseResponse>
{
    public Guid Id { get; set; }

    public GetByIdQueryClientUseCase(Guid id) => Id = id;
}

 
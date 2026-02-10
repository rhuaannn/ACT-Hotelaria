using ACT_Hotelaria.Application.Abstract.Query;
using ACT_Hotelaria.Domain.Repository.ProductRepository;
using MediatR;

namespace ACT_Hotelaria.Application.UseCase.Product.GetAll;

public class GetAllProductUseCase : IQueryHandler<GetAllQueryProduct, IEnumerable<GetAllProductUseCaseResponse>>
{
    private readonly IReadOnlyProductRepository _readOnlyProductRepository;

    public GetAllProductUseCase(IReadOnlyProductRepository readOnlyProductRepository)
    {
        _readOnlyProductRepository = readOnlyProductRepository;
        
    }
    
    public async Task<IEnumerable<GetAllProductUseCaseResponse>> Handle(GetAllQueryProduct query, CancellationToken cancellationToken)
    {
        var productAll = await _readOnlyProductRepository.GetAll();
        var response = productAll.Select(product => new GetAllProductUseCaseResponse
        {
            Id = product.Id,
            Name = product.Name,
            Value = product.ValueProduct
        });
        return response;
    }
}
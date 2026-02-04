 using ACT_Hotelaria.Domain.Repository.ProductRepository;
 using ACT_Hotelaria.SqlServer.Repository;

namespace ACT_Hotelaria.Application.UseCase.Product;

public class RegisterProductUseCase
{
    private readonly IWriteOnlyProductRepository _IWiriteProductRepository;

    public RegisterProductUseCase(IWriteOnlyProductRepository iWriteOnlyProductRepository)
    {
        _IWiriteProductRepository = iWriteOnlyProductRepository;
    }

    public async Task<RegisterProductUseCaseResponse> Handle(RegisterProductUseCaseRequest request)
    {
       var product = Domain.Entities.Product.Create(request.Name, request.Quantity, request.Value);
       await _IWiriteProductRepository.Add(product);
        
       return new RegisterProductUseCaseResponse
       {
           Name = product.Name,
           Value = request.Value,
       };
    }
}
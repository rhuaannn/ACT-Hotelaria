 using ACT_Hotelaria.Domain.Repository.ProductRepository;
 using ACT_Hotelaria.SqlServer.Repository;
 using MediatR;
 using Microsoft.Extensions.Logging;

 namespace ACT_Hotelaria.Application.UseCase.Product;

public class RegisterProductUseCase : IRequestHandler<RegisterProductUseCaseRequest, RegisterProductUseCaseResponse>
{
    private readonly IWriteOnlyProductRepository _IWiriteProductRepository;
    private readonly ILogger<RegisterProductUseCase> _logger;

    public RegisterProductUseCase(IWriteOnlyProductRepository iWriteOnlyProductRepository, ILogger<RegisterProductUseCase> logger)
    {
        _IWiriteProductRepository = iWriteOnlyProductRepository;
        _logger = logger;
    }

    public async Task<RegisterProductUseCaseResponse> Handle(RegisterProductUseCaseRequest request, CancellationToken cancellationToken)
    {
       var product = Domain.Entities.Product.Create(request.Name, request.Quantity, request.Value);
       await _IWiriteProductRepository.Add(product);
        _logger.LogInformation($"Produto {product.Name} cadastrado com sucesso");
       return new RegisterProductUseCaseResponse
       {
           Name = product.Name,
           Value = request.Value,
       };
    }
}
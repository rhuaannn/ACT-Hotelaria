using ACT_Hotelaria.Domain.Abstract;
using ACT_Hotelaria.Domain.Repository.ProductRepository;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ACT_Hotelaria.Application.UseCase.Product;

public class RegisterProductUseCase : IRequestHandler<RegisterProductUseCaseRequest, RegisterProductUseCaseResponse>
{
    private readonly IWriteOnlyProductRepository _IWiriteProductRepository;
    private readonly ILogger<RegisterProductUseCase> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterProductUseCase(IWriteOnlyProductRepository WriteOnlyProductRepository, 
        ILogger<RegisterProductUseCase> logger,
        IUnitOfWork unitOfWork)
    {
        _IWiriteProductRepository = WriteOnlyProductRepository;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<RegisterProductUseCaseResponse> Handle(RegisterProductUseCaseRequest request, CancellationToken cancellationToken)
    {
       var product = Domain.Entities.Product.Create(request.Name, request.Quantity, request.Value);
       await _IWiriteProductRepository.Add(product);
       await _unitOfWork.CommitAsync(cancellationToken);
        _logger.LogInformation($"Produto {product.Name} cadastrado com sucesso");
       return new RegisterProductUseCaseResponse
       {
           Name = product.Name,
           Value = request.Value,
       };
    }
}
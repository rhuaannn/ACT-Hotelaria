using ACT_Hotelaria.Domain.Abstract;
using ACT_Hotelaria.Domain.Exception;
using MediatR;
using Microsoft.EntityFrameworkCore.Storage;

namespace ACT_Hotelaria.SqlServer;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly ACT_HotelariaDbContext _context;
    private IDbContextTransaction? _transaction;
    private readonly IPublisher _publisher;
    
    public UnitOfWork(ACT_HotelariaDbContext context)
    {
        _context = context;
    }
 
    public async Task<int> Commit()
    {
        var result =  await _context.SaveChangesAsync();
            if(_transaction == null)
            {
                throw new DomainException("Erro ao salvar dados");
            }
            return result;
    }

    public async Task Rollback()
    {
        if (_transaction == null)
        {
            return;
        }
        await _transaction.RollbackAsync();
        await _transaction.DisposeAsync();
        _transaction = null;
    }
    
    public void Dispose() => _context.Dispose();
}
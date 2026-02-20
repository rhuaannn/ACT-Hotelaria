using ACT_Hotelaria.Domain.Abstract;
using Microsoft.EntityFrameworkCore.Storage;

namespace ACT_Hotelaria.SqlServer;
public sealed class UnitOfWork : IUnitOfWork
{
    private readonly ACT_HotelariaDbContext _context;
    private IDbContextTransaction? _transaction;

    public UnitOfWork(ACT_HotelariaDbContext context)
    {
        _context = context;
    }
    public async Task BeginTransaction(CancellationToken cancellationToken = default)
    {
        if (_transaction != null) return;

        _transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
    }
    public async Task RollbackAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction == null) return;

        await _transaction.RollbackAsync(cancellationToken);
        await _transaction.DisposeAsync();
        _transaction = null;
    }
    public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var changes = await _context.SaveChangesAsync(cancellationToken);

            if (_transaction != null)
            {
                await _transaction.CommitAsync(cancellationToken);
                await _transaction.DisposeAsync();
                _transaction = null;
            }

            return changes;
        }
        catch
        {
            await RollbackAsync(cancellationToken);
            throw;
        }
    }
    public void Dispose()
    {
        _transaction?.Dispose();
        GC.SuppressFinalize(this);
    }

    public async ValueTask DisposeAsync()
    {
        if (_transaction != null)
        {
            await _transaction.DisposeAsync();
        }

        GC.SuppressFinalize(this);
    }
    
}
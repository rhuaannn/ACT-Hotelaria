namespace ACT_Hotelaria.Domain.Abstract;

public interface IUnitOfWork : IDisposable
{
    Task<int> Commit();
    Task Rollback();
}
namespace ACT_Hotelaria.Domain.Repository.cs.Reservation;

public interface IReadOnlyReservationRepository
{
    public Task<Entities.Reservation> GetAllById(Guid id);
    public Task<IEnumerable<Entities.Reservation>> GetAll();
    public Task<bool> Exists(Guid id);
    public Task<bool> ExistsCheckin(DateTime checkin);
    public Task<bool> ExistsCheckout(DateTime checkout);
}
namespace ACT_Hotelaria.Domain.Repository.Reservation;

public interface IWriteOnlyReservationRepository
{
    public Task Add(Entities.Reservation reservation);
    public Task<bool> Remove(Guid id);
    public Task Update(Entities.Reservation reservation);
}
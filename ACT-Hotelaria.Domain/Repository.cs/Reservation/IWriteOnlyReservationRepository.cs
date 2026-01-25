namespace ACT_Hotelaria.Domain.Repository.cs.Reservation;

public interface IWriteOnlyReservationRepository
{
    public Task Add(Entities.Reservation reservation);
    public Task<bool> Remove(Guid id);
    public void Update(Entities.Reservation reservation);
}
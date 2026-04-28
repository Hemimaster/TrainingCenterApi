using TrainingCenterApi.Data;
using TrainingCenterApi.Models;

namespace TrainingCenterApi.Services;

public class ReservationsService
{
    public IEnumerable<Reservation> GetAll()
    {
        return TrainingCenterData.Reservations;
    }
    
    public Reservation? GetById(int id)
    {
        return TrainingCenterData.Reservations.FirstOrDefault(r => r.Id == id);
    }
    
    public IEnumerable<Reservation> GetFiltered(DateTime? date, string? status, int? roomId)
    {
        var reservations = TrainingCenterData.Reservations.AsEnumerable();

        if (date.HasValue)
        {
            reservations = reservations.Where(r => r.Date.Date == date.Value.Date);
        }

        if (!string.IsNullOrWhiteSpace(status))
        {
            reservations = reservations.Where(r => string.Equals(
                r.Status,
                status,
                StringComparison.OrdinalIgnoreCase));
        }

        if (roomId.HasValue)
        {
            reservations = reservations.Where(r => r.RoomId == roomId.Value);
        }

        return reservations;
    }
    
    private bool HasConflict(Reservation newReservation)
    {
        return TrainingCenterData.Reservations.Any(r =>
            r.RoomId == newReservation.RoomId &&
            r.Date.Date == newReservation.Date.Date &&
            newReservation.StartTime < r.EndTime &&
            newReservation.EndTime > r.StartTime
        );
    }
    
    private bool HasConflict(Reservation updatedReservation, int ignoredReservationId)
    {
        return TrainingCenterData.Reservations.Any(r =>
            r.Id != ignoredReservationId &&
            r.RoomId == updatedReservation.RoomId &&
            r.Date.Date == updatedReservation.Date.Date &&
            updatedReservation.StartTime < r.EndTime &&
            updatedReservation.EndTime > r.StartTime
        );
    }
    
    public (bool Success, string? Error, Reservation? Reservation) Add(Reservation reservation)
    {
        var room = TrainingCenterData.Rooms.FirstOrDefault(r => r.Id == reservation.RoomId);

        if (room is null)
        {
            return (false, "Room does not exist", null);
        }

        if (!room.IsActive)
        {
            return (false, "Room is not active", null);
        }

        if (HasConflict(reservation))
        {
            return (false, "Time conflict with existing reservation", null);
        }

        var newId = TrainingCenterData.Reservations.Any()
            ? TrainingCenterData.Reservations.Max(r => r.Id) + 1
            : 1;

        reservation.Id = newId;

        TrainingCenterData.Reservations.Add(reservation);

        return (true, null, reservation);
    }
    
    public (bool Success, string? Error, Reservation? Reservation) Update(int id, Reservation updatedReservation)
    {
        var existingReservation = GetById(id);

        if (existingReservation is null)
        {
            return (false, "Reservation does not exist", null);
        }

        var room = TrainingCenterData.Rooms.FirstOrDefault(r => r.Id == updatedReservation.RoomId);

        if (room is null)
        {
            return (false, "Room does not exist", null);
        }

        if (!room.IsActive)
        {
            return (false, "Room is not active", null);
        }

        if (HasConflict(updatedReservation, id))
        {
            return (false, "Time conflict with existing reservation", null);
        }

        existingReservation.RoomId = updatedReservation.RoomId;
        existingReservation.OrganizerName = updatedReservation.OrganizerName;
        existingReservation.Topic = updatedReservation.Topic;
        existingReservation.Date = updatedReservation.Date;
        existingReservation.StartTime = updatedReservation.StartTime;
        existingReservation.EndTime = updatedReservation.EndTime;
        existingReservation.Status = updatedReservation.Status;

        return (true, null, existingReservation);
    }
    
    public bool Delete(int id)
    {
        var reservation = GetById(id);

        if (reservation is null)
        {
            return false;
        }

        TrainingCenterData.Reservations.Remove(reservation);

        return true;
    }
}
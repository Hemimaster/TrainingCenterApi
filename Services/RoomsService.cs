using TrainingCenterApi.Data;
using TrainingCenterApi.Models;

namespace TrainingCenterApi.Services;

public class RoomsService
{
    public IEnumerable<Room> GetAll()
    {
        return TrainingCenterData.Rooms;
    }
    
    public Room? GetById(int id)
    {
        return TrainingCenterData.Rooms.FirstOrDefault(r => r.Id == id);
    }
    
    public IEnumerable<Room> GetFiltered(int? minCapacity, bool? hasProjector, bool? activeOnly)
    {
        var rooms = TrainingCenterData.Rooms.AsEnumerable();

        if (minCapacity.HasValue)
        {
            rooms = rooms.Where(r => r.Capacity >= minCapacity.Value);
        }

        if (hasProjector.HasValue)
        {
            rooms = rooms.Where(r => r.HasProjector == hasProjector.Value);
        }

        if (activeOnly == true)
        {
            rooms = rooms.Where(r => r.IsActive);
        }

        return rooms;
    }
    
    public IEnumerable<Room> GetByBuildingCode(string buildingCode)
    {
        return TrainingCenterData.Rooms
            .Where(r => string.Equals(
                r.BuildingCode,
                buildingCode,
                StringComparison.OrdinalIgnoreCase));
    }
    
    public Room Add(Room room)
    {
        var newId = TrainingCenterData.Rooms.Any()
            ? TrainingCenterData.Rooms.Max(r => r.Id) + 1
            : 1;

        room.Id = newId;

        TrainingCenterData.Rooms.Add(room);

        return room;
    }
    
    public bool Update(int id, Room updatedRoom)
    {
        var existingRoom = GetById(id);

        if (existingRoom is null)
        {
            return false;
        }

        existingRoom.Name = updatedRoom.Name;
        existingRoom.BuildingCode = updatedRoom.BuildingCode;
        existingRoom.Floor = updatedRoom.Floor;
        existingRoom.Capacity = updatedRoom.Capacity;
        existingRoom.HasProjector = updatedRoom.HasProjector;
        existingRoom.IsActive = updatedRoom.IsActive;

        return true;
    }
    
    public bool HasReservations(int roomId)
    {
        return TrainingCenterData.Reservations.Any(r => 
            r.RoomId == roomId &&
            r.Status != "cancelled");
    }

    public bool Delete(int id)
    {
        var room = GetById(id);

        if (room is null)
        {
            return false;
        }

        TrainingCenterData.Rooms.Remove(room);

        return true;
    }
    
}


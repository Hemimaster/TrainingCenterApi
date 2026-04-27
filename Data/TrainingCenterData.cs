using TrainingCenterApi.Models;

namespace TrainingCenterApi.Data;

public static class TrainingCenterData
{
    public static List<Room> Rooms { get; set; } = 
    [
        new Room
        {
            Id = 1,
            Name = "Room A1",
            BuildingCode = "A",
            Floor = 1,
            Capacity = 20,
            HasProjector = true,
            IsActive = true
        },
        new Room
        {
            Id = 2,
            Name = "Room A2",
            BuildingCode = "A",
            Floor = 2,
            Capacity = 15,
            HasProjector = false,
            IsActive = true
        },
        new Room
        {
            Id = 3,
            Name = "Room B1",
            BuildingCode = "B",
            Floor = 1,
            Capacity = 30,
            HasProjector = true,
            IsActive = true
        },
        new Room
        {
            Id = 4,
            Name = "Room C1",
            BuildingCode = "C",
            Floor = 3,
            Capacity = 25,
            HasProjector = false,
            IsActive = false
        },
        new Room
        {
        Id = 5,
        Name = "Room B2",
        BuildingCode = "B",
        Floor = 2,
        Capacity = 18,
        HasProjector = true,
        IsActive = true
        }
    ];
    public static List<Reservation> Reservations { get; set; } = 
    [
        new Reservation
        {
            Id = 1,
            RoomId = 1,
            OrganizerName = "Anna Kowalska",
            Topic = "HTTP Basics",
            Date = new DateTime(2026, 5, 10),
            StartTime = new TimeOnly(9, 0),
            EndTime = new TimeOnly(11, 0),
            Status = "confirmed"
        },
        new Reservation
        {
            Id = 2,
            RoomId = 1,
            OrganizerName = "Jan Nowak",
            Topic = "REST API",
            Date = new DateTime(2026, 5, 10),
            StartTime = new TimeOnly(11, 30),
            EndTime = new TimeOnly(13, 0),
            Status = "planned"
        },
        new Reservation
        {
            Id = 3,
            RoomId = 2,
            OrganizerName = "Maria Wiśniewska",
            Topic = "C# Advanced",
            Date = new DateTime(2026, 5, 11),
            StartTime = new TimeOnly(10, 0),
            EndTime = new TimeOnly(12, 0),
            Status = "confirmed"
        },
        new Reservation
        {
            Id = 4,
            RoomId = 3,
            OrganizerName = "Piotr Zieliński",
            Topic = "Databases",
            Date = new DateTime(2026, 5, 12),
            StartTime = new TimeOnly(8, 0),
            EndTime = new TimeOnly(10, 0),
            Status = "cancelled"
        },
        new Reservation
        {
            Id = 5,
            RoomId = 5,
            OrganizerName = "Katarzyna Lewandowska",
            Topic = "ASP.NET Core",
            Date = new DateTime(2026, 5, 13),
            StartTime = new TimeOnly(14, 0),
            EndTime = new TimeOnly(16, 0),
            Status = "confirmed"
        },
        new Reservation
        {
            Id = 6,
            RoomId = 2,
            OrganizerName = "Tomasz Wójcik",
            Topic = "Docker Basics",
            Date = new DateTime(2026, 5, 14),
            StartTime = new TimeOnly(9, 30),
            EndTime = new TimeOnly(11, 30),
            Status = "planned"
        }
    ];   
}
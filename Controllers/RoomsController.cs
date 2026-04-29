using Microsoft.AspNetCore.Mvc;
using TrainingCenterApi.Services;
using TrainingCenterApi.Models;

namespace TrainingCenterApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomsController : ControllerBase
{
    private readonly RoomsService _roomsService;

    public RoomsController(RoomsService roomsService)
    {
        _roomsService = roomsService;
    }
    
    [HttpGet]
    public IActionResult GetAll(
        [FromQuery] int? minCapacity,
        [FromQuery] bool? hasProjector,
        [FromQuery] bool? activeOnly)
    {
        var rooms = _roomsService.GetFiltered(minCapacity, hasProjector, activeOnly);
        return Ok(rooms);
    }
    
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var room = _roomsService.GetById(id);

        if (room is null)
        {
            return NotFound();
        }

        return Ok(room);
    }
    
    [HttpGet("building/{buildingCode}")]
    public IActionResult GetByBuildingCode(string buildingCode)
    {
        var rooms = _roomsService.GetByBuildingCode(buildingCode);
        return Ok(rooms);
    }
    
    [HttpPost]
    public IActionResult Add(Room room)
    {
        var createdRoom = _roomsService.Add(room);
        return CreatedAtAction(nameof(GetById), new { id = createdRoom.Id }, createdRoom);
    }
    
    [HttpPut("{id}")]
    public IActionResult Update(int id, Room room)
    {
        var updated = _roomsService.Update(id, room);

        if (!updated)
        {
            return NotFound();
        }

        return Ok();
    }
    
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var room = _roomsService.GetById(id);

        if (room is null)
        {
            return NotFound();
        }

        if (_roomsService.HasReservations(id))
        {
            return Conflict("Room has reservations and cannot be deleted");
        }

        _roomsService.Delete(id);

        return NoContent();
    }
}
using Microsoft.AspNetCore.Mvc;
using TrainingCenterApi.Models;
using TrainingCenterApi.Services;

namespace TrainingCenterApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationsController : ControllerBase
{
    private readonly ReservationsService _reservationsService;

    public ReservationsController(ReservationsService reservationsService)
    {
        _reservationsService = reservationsService;
    }
    
    [HttpGet]
    public IActionResult GetAll(
        [FromQuery] DateOnly? date,
        [FromQuery] string? status,
        [FromQuery] int? roomId)
    {
        var reservations = _reservationsService.GetFiltered(date, status, roomId);
        return Ok(reservations);
    }
    
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var reservation = _reservationsService.GetById(id);

        if (reservation is null)
        {
            return NotFound();
        }

        return Ok(reservation);
    }
    
    [HttpPost]
    public IActionResult Add(Reservation reservation)
    {
        var result = _reservationsService.Add(reservation);

        if (!result.Success)
        {
            if (result.Error == "Room does not exist")
            {
                return NotFound(result.Error);
            }

            return Conflict(result.Error);
        }

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.Reservation!.Id },
            result.Reservation);
    }
    
    [HttpPut("{id}")]
    public IActionResult Update(int id, Reservation reservation)
    {
        var result = _reservationsService.Update(id, reservation);

        if (!result.Success)
        {
            if (result.Error == "Reservation does not exist")
            {
                return NotFound(result.Error);
            }

            if (result.Error == "Room does not exist")
            {
                return NotFound(result.Error);
            }

            return Conflict(result.Error);
        }

        return Ok(result.Reservation);
    }
    
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var deleted = _reservationsService.Delete(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
    
}
using Microsoft.AspNetCore.Mvc;
using MeetM.Models;
using MeetS.Services;
using BeThere.Dtos;

namespace BeThere.Controllers;

[ApiController]
[Route("bt/meet")]

public class BeThereController : ControllerBase
{
    private readonly MeetService _meetService;
    public BeThereController(MeetService meetService)
    {
        _meetService = meetService;
    }

    [HttpGet]
    public List<Meet> GetMeets()
    {
        return _meetService.GetMeets();
    }

    [HttpGet("{meetId}")]
    public ActionResult<Meet> GetMeetById(int meetId)
    {
        Meet? meet = _meetService.GetMeetById(meetId);
        if (meet == null)
        {
            return NotFound();
        }

        return meet;
    }

    [HttpPost]
    public ActionResult<Meet> CreateMeet([FromBody] CreateMeetDto dto)
    {
        Meet meet = new Meet
    {
        Name = dto.Name,
        Location = dto.Location,
        StartDate = dto.StartDate,
        EndDate = dto.EndDate,
        TimeZoneId = dto.TimeZoneId
    };

    var createdMeet = _meetService.CreateMeet(meet);

    return CreatedAtAction(
        nameof(GetMeetById),
        new { meetId = createdMeet.Id },
        createdMeet);
    }

    [HttpPut("{meetId}")]
    public ActionResult<Meet> UpdateMeet(int meetId, [FromBody] UpdateMeetDto dto)
    {
        Meet meet = new Meet
        {
            Name = dto.Name,
            Location = dto.Location,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            TimeZoneId = dto.TimeZoneId
        };

        Meet? updatedMeet = _meetService.UpdateMeet(meetId, meet);
        if (updatedMeet == null)
        {
            return NotFound();
        }

        return Ok(updatedMeet);
    }

    [HttpDelete("{meetId}")]
    public IActionResult DeleteMeet(int meetId)
    {
        bool deleted = _meetService.DeleteMeet(meetId);
        if (!deleted)
    {
            return NotFound();
        }
        
        return NoContent();
    }
}
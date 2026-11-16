using MeetDb.Data;
using MeetM.Models;

namespace MeetS.Services;

public class MeetService
{
    private readonly MeetDbContext _context;

    public MeetService(MeetDbContext context)
    {
        _context = context;
    }

    public List<Meet> GetMeets()
    {
        return _context.Meets.ToList();
    }

    public Meet? GetMeetById(int meetId)
    {
        return _context.Meets.Find(meetId);
    }

    public Meet CreateMeet(Meet meet)
    {
        Meet newMeet = new Meet
        {
            Name = meet.Name,
            Location = meet.Location,
            StartDate = meet.StartDate,
            EndDate = meet.EndDate,
            TimeZoneId = meet.TimeZoneId
        };

        _context.Meets.Add(newMeet);

        _context.SaveChanges();

        return newMeet;
    }
}
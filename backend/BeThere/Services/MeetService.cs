using MeetCG.Services;
using MeetDb.Data;
using MeetM.Models;

namespace MeetS.Services;

public class MeetService
{
    private readonly MeetDbContext _context;
    private readonly MeetCodeGenerator _codeGenerator;

    public MeetService(MeetDbContext context, MeetCodeGenerator codeGenerator)
    {
        _context = context;
        _codeGenerator = codeGenerator;
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
        string meetCode;

        do
        {
            meetCode = _codeGenerator.Generate();
        }
        while (_context.Meets.Any(meet => meet.MeetCode == meetCode));

        Meet newMeet = new Meet
        {
            Name = meet.Name,
            Location = meet.Location,
            StartDate = meet.StartDate,
            EndDate = meet.EndDate,
            TimeZoneId = meet.TimeZoneId,
            MeetCode = meetCode
        };

        _context.Meets.Add(newMeet);

        _context.SaveChanges();

        return newMeet;
    }

    public Meet? UpdateMeet(int meetId, Meet meet)
    {
        Meet? existingMeet = _context.Meets.Find(meetId);
        if (existingMeet == null)
        {
            return null;
        }

        existingMeet.Name = meet.Name;
        existingMeet.Location = meet.Location;
        existingMeet.StartDate = meet.StartDate;
        existingMeet.EndDate = meet.EndDate;
        existingMeet.TimeZoneId = meet.TimeZoneId;

        _context.SaveChanges();

        return existingMeet;
    }

    public bool DeleteMeet(int meetId)
    {
        Meet? meet = _context.Meets.Find(meetId);
        if (meet == null)
        {
            return false;
        }

        _context.Meets.Remove(meet);
        _context.SaveChanges();

        return true;
    }
}

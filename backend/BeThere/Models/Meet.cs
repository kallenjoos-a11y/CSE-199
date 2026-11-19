namespace MeetM.Models;
using System.ComponentModel.DataAnnotations;
using MeetCG.Services;

public class Meet : IValidatableObject
{
    public int Id { get; set; }

    [StringLength(150)]
    public string Name { get; set; } = string.Empty;

    [StringLength(150)]
    public string Location { get; set; } = string.Empty;

    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }

    [StringLength(100)]
    public string TimeZoneId { get; set; } = string.Empty;
    public string MeetCode { get; set; } = string.Empty;

    public IEnumerable<ValidationResult> Validate(
        ValidationContext validationContext)
    {
        if (string.IsNullOrWhiteSpace(Name))
        {
            yield return new ValidationResult(
                "Meet name is required.",
                new[] { nameof(Name) });
        }

        if (string.IsNullOrWhiteSpace(Location))
        {
            yield return new ValidationResult(
                "Meet location is required.",
                new[] { nameof(Location) });
        }

        if (StartDate == default)
        {
            yield return new ValidationResult(
                "Start date is required.",
                new[] { nameof(StartDate) });
        }

        if (EndDate == default)
        {
            yield return new ValidationResult(
                "End date is required.",
                new[] { nameof(EndDate) });
        }

        if (StartDate != default &&
            EndDate != default &&
            StartDate > EndDate)
        {
            yield return new ValidationResult(
                "Start date cannot be after end date.",
                new[] { nameof(StartDate), nameof(EndDate) });
        }

        string? timeZoneError = null;

        if (string.IsNullOrWhiteSpace(TimeZoneId))
        {
            timeZoneError = "Time zone is required.";
        }
        else
        {
            try
            {
                TimeZoneInfo.FindSystemTimeZoneById(TimeZoneId);
            }
            catch (TimeZoneNotFoundException)
            {
                timeZoneError = "The specified time zone was not found.";
            }
            catch (InvalidTimeZoneException)
            {
                timeZoneError = "The specified time zone is invalid.";
            }
        }

        if (timeZoneError is not null)
        {
            yield return new ValidationResult(
                timeZoneError,
                new[] { nameof(TimeZoneId) });
        }
    }
}
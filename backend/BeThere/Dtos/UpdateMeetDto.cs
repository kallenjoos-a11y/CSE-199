using System.ComponentModel.DataAnnotations;

namespace BeThere.Dtos;

public class UpdateMeetDto : IValidatableObject
{
    [Required]
    [StringLength(150)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(150)]
    public string Location { get; set; } = string.Empty;

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    [Required]
    [StringLength(100)]
    public string TimeZoneId { get; set; } = string.Empty;

    public IEnumerable<ValidationResult> Validate(
        ValidationContext validationContext)
    {
        var errors = new List<ValidationResult>();

        if (string.IsNullOrWhiteSpace(Name))
        {
            errors.Add(new ValidationResult(
                "Meet name is required.",
                new[] { nameof(Name) }));
        }

        if (string.IsNullOrWhiteSpace(Location))
        {
            errors.Add(new ValidationResult(
                "Meet location is required.",
                new[] { nameof(Location) }));
        }

        if (StartDate == default)
        {
            errors.Add(new ValidationResult(
                "Start date is required.",
                new[] { nameof(StartDate) }));
        }

        if (EndDate == default)
        {
            errors.Add(new ValidationResult(
                "End date is required.",
                new[] { nameof(EndDate) }));
        }

        if (StartDate != default &&
            EndDate != default &&
            StartDate > EndDate)
        {
            errors.Add(new ValidationResult(
                "Start date cannot be after end date.",
                new[] { nameof(StartDate), nameof(EndDate) }));
        }

        if (string.IsNullOrWhiteSpace(TimeZoneId))
        {
            errors.Add(new ValidationResult(
                "Time zone is required.",
                new[] { nameof(TimeZoneId) }));
        }
        else
        {
            try
            {
                TimeZoneInfo.FindSystemTimeZoneById(TimeZoneId);
            }
            catch (TimeZoneNotFoundException)
            {
                errors.Add(new ValidationResult(
                    "The specified time zone was not found.",
                    new[] { nameof(TimeZoneId) }));
            }
            catch (InvalidTimeZoneException)
            {
                errors.Add(new ValidationResult(
                    "The specified time zone is invalid.",
                    new[] { nameof(TimeZoneId) }));
            }
        }

        return errors;
    }
}
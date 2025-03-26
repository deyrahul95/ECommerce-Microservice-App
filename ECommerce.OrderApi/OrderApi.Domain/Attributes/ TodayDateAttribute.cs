using System.ComponentModel.DataAnnotations;

namespace OrderApi.Domain.Attributes;

public class TodayDateAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        // Check if the value is null
        if (value == null)
        {
            return new ValidationResult("OrderDate is required.");
        }

        // Check if the value is a DateTime
        if (value is DateTime dateTime)
        {
            if (dateTime.Date == DateTime.UtcNow.Date)
            {
                return ValidationResult.Success!;
            }
            else
            {
                return new ValidationResult("OrderDate must be today's date.");
            }
        }

        return new ValidationResult("Invalid date format.");
    }
}

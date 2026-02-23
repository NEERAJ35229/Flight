using System;
using System.ComponentModel.DataAnnotations;

namespace Flight.Models
{
    // ================================
    // Custom Validation Attribute
    // ================================
    public class NotEqualTo : ValidationAttribute
    {
        private readonly string _otherProperty;

        public NotEqualTo(string otherProperty)
        {
            _otherProperty = otherProperty;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var otherValue = validationContext.ObjectType
                .GetProperty(_otherProperty)?
                .GetValue(validationContext.ObjectInstance);

            if (value != null && value.Equals(otherValue))
            {
                return new ValidationResult(
                    ErrorMessage ?? $"Cannot be the same as {_otherProperty}."
                );
            }

            return ValidationResult.Success;
        }
    }

    // ================================
    // Flight Model
    // ================================
    public class FlightModel
    {
        [Key]
        public int FlightId { get; set; }

        [Required]
        [StringLength(10)]
        public string FlightNumber { get; set; } = string.Empty;

        [Required]
        public string From { get; set; } = string.Empty;

        [Required]
        [NotEqualTo("From", ErrorMessage = "Destination cannot be the same as departure city.")]
        public string To { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        [Range(1, 100000, ErrorMessage = "Price must be greater than 0.")]
        public decimal Price { get; set; }

        // ================================
        // SAFE Slug Property (No Crash)
        // ================================
        public string Slug
        {
            get
            {
                if (string.IsNullOrWhiteSpace(FlightNumber))
                    return FlightId.ToString();

                return FlightNumber
                        .Replace(" ", "-")
                        .ToLower() + "-" + FlightId;
            }
        }
    }
}
﻿using System.ComponentModel.DataAnnotations;

namespace RoomBookingApp.Domain.BaseModels
{
    public abstract class RoomBookingBase : IValidatableObject
    {
        [Required]
        [StringLength(80)]
        public string FullName { get; set; } = string.Empty;
        [Required]
        [StringLength(80)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [DataType(DataType.Date)]
        public DateTime Date { get; set; } = DateTime.Now;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(Date <= DateTime.Now.Date)
            {
                yield return new ValidationResult("Date MUST be in the future", new[] { nameof(Date) });
            }
        }
    }
}
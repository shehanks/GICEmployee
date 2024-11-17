using GICEmployee.Common.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GICEmployee.Domain.Entities
{
    public class Employee
    {
        [Key]
        [Required]
        [RegularExpression(@"UI[A-Za-z0-9]{7}", ErrorMessage = "Invalid ID format")]
        [MaxLength(9)]
        public string Id { get; set; } = string.Empty; // Example: UI1234567

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [MaxLength(320)]
        public string EmailAddress { get; set; } = string.Empty;

        [Required]
        [RegularExpression(@"^[89][0-9]{7}$", ErrorMessage = "Invalid phone number format")]
        [MaxLength(8)]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        [EnumDataType(typeof(Gender))]
        [MaxLength(1)]
        public Gender Gender { get; set; }

        // Navigation Property
        [JsonIgnore]
        public virtual EmployeeCafeRelationship? EmployeeCafeRelationship { get; set; }

    }
}

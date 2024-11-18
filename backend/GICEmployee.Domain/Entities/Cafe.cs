using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GICEmployee.Domain.Entities
{
    public class Cafe
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(500)]
        public string Description { get; set; } = string.Empty;

        public byte[]? Logo { get; set; } // Optional, stored as byte array for image

        [Required]
        [StringLength(200)]
        public string Location { get; set; } = string.Empty;

        // Navigation Property
        [JsonIgnore]
        public virtual ICollection<EmployeeCafeRelationship>? EmployeeCafeRelationships { get; set; }

        // Constructor to initialize navigation properties
        public Cafe()
        {
            // Initialize navigation property to an empty collection
            EmployeeCafeRelationships = new List<EmployeeCafeRelationship>();
        }
    }
}

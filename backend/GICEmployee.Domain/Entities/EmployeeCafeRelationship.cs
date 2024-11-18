using System.ComponentModel.DataAnnotations;

namespace GICEmployee.Domain.Entities
{
    public class EmployeeCafeRelationship
    {
        [Key]
        public string EmployeeId { get; set; } = string.Empty; // Foreign Key

        [Key]
        public Guid CafeId { get; set; } // Foreign Key

        [Required]
        public DateTime? StartDate { get; set; }

        // Navigation Properties
        public virtual Employee? Employee { get; set; }
        public virtual Cafe? Cafe { get; set; }
    }
}

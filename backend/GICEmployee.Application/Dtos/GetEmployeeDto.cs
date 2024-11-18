namespace GICEmployee.Application.Dtos
{
    public class GetEmployeeDto
    {
        public string Id { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string EmailAddress { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public int DaysWorked { get; set; }

        public string? CafeName { get; set; }
    }
}

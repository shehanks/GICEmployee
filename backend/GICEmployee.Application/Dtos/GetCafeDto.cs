﻿namespace GICEmployee.Application.Dtos
{
    public class GetCafeDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public int EmployeeCount { get; set; }

        public string Location { get; set; } = string.Empty;

        public string? ImageBase64 { get; set; }
    }
}
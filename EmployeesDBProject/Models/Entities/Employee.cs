﻿using System.ComponentModel.DataAnnotations;

namespace EmployeesDBProject.Models.Entities
{
    public class Employee
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
    }
}

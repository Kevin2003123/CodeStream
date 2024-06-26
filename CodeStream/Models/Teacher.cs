﻿using System.ComponentModel.DataAnnotations;

namespace CodeStream.Models
{
    public class Teacher
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Phone { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
    }
}

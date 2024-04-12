﻿using System.ComponentModel.DataAnnotations;

namespace CodeStream.Models.DTO
{
    public class FormLevelDTO
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;
      
    }
}

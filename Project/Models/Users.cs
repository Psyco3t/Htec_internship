﻿using System.ComponentModel.DataAnnotations;
namespace Project.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [MaxLength(100)]
        public string Email { get; set; }
    }
}

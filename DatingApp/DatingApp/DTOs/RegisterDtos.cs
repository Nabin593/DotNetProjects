﻿using System.ComponentModel.DataAnnotations;

namespace DatingApp.DTOs
{
    public class RegisterDtos
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [StringLength(8, MinimumLength = 4)]
        public string Password { get; set; }
    }
}

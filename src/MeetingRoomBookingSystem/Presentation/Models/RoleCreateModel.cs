﻿using System.ComponentModel.DataAnnotations;

namespace Presentation.Models
{
    public class RoleCreateModel
    {
        [Required]
        public string Name { get; set; }
    }
}

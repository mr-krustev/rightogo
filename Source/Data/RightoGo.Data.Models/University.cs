﻿namespace RightoGo.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Common.Models;

    public class University : BaseModel<int>
    {
        [Required]
        [Index(IsUnique = true)]
        [MinLength(2)]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}

﻿using System.ComponentModel.DataAnnotations;

namespace ImagineBookStore.Core.Models.App;

public class Role
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    public ICollection<UserRole> UserRoles { get; set; }
}
﻿using System.Collections.Generic;

namespace ThalesAssessment.Entities;

public class Person
{
    public int Id { get ; set; }

    public string Name { get; set; } = string.Empty;

    public List<Role> Roles { get; set; } = new();
}

using System.Collections.Generic;

namespace ThalesAssessment.Entities;

public class Role
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public List<Person> Persons { get; set;} = new();
}

using System.Collections.Generic;

namespace ThalesAssessment.Cqrs.Person;

public class CreatePerson
{
    public string Name { get; set; } = string.Empty;

    public List<int> RoleIds { get; set; } = new();
}

using System.Collections.Generic;
using ThalesAssessment.Entities;

namespace ThalesAssessment.ApiModels.RequestModels;

public class CreatePerson
{
    public string Name { get; set; } = string.Empty;

    public List<int> RoleIds { get; set; } = new();

    public Person ToPerson()
    {
        return new Person
        {
            Name = Name
        };
    }
}

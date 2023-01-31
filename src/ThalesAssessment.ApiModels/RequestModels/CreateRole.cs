using System.Collections.Generic;
using ThalesAssessment.Entities;

namespace ThalesAssessment.ApiModels.RequestModels;

public class CreateRole
{
    public string Name { get; set; } = string.Empty;

    public List<int> PersonIds { get; set; } = new();

    public Role ToRole()
    {
        return new Role
        {
            Name = Name
        };
    }
}

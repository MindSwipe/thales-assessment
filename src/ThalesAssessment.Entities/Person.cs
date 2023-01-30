using System.Collections.Generic;
using System.Linq;

namespace ThalesAssessment.Entities;

public class Person
{
    public int Id { get ; set; }

    public string Name { get; set; } = string.Empty;

    public List<Role> Roles { get; set; } = new();

    public void AssignRole(Role role)
    {
        if (Roles.Select(x => x.Id).Contains(role.Id))
            return;

        Roles.Add(role);
    }
}

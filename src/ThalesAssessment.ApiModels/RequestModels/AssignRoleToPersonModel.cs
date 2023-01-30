using ThalesAssessment.Entities;

namespace ThalesAssessment.ApiModels.RequestModels;

public class AssignRoleToPersonModel
{
    public Person Person { get; set; }

    public int RoleId { get; set; }
}

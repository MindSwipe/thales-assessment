using ThalesAssessment.Entities;

namespace ThalesAssessment.ApiModels.RequestModels;

public class AssignRoleToPersonModel
{
    public int PersonId { get; set; }

    public int RoleId { get; set; }
}

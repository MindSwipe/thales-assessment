using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThalesAssessment.DataAccess;
using ThalesAssessment.Entities;
using ThalesAssessment.Queries;

namespace ThalesAssessment.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class RoleController : ControllerBase
{
    private readonly AssessmentContext _dbContext;

    public RoleController(AssessmentContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    [Route("create")]
    public async Task Create([FromBody] Role role)
    {
        _dbContext.Roles.Update(role);
        await _dbContext.SaveChangesAsync();
    }

    [HttpGet]
    [Route("getAll")]
    public async Task<List<Role>> GetAllRoles()
    {
        return await RoleQuerier.GetAll(_dbContext);
    }
}

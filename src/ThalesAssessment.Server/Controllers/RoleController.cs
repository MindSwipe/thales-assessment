using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThalesAssessment.ApiModels.RequestModels;
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
    public async Task Create([FromBody] CreateRole roleRequest)
    {
        var role = roleRequest.ToRole();
        var persons = await PersonQuerier.GetByIds(_dbContext, roleRequest.PersonIds);
        role.Persons = persons;

        _dbContext.Roles.Update(role);
        await _dbContext.SaveChangesAsync();
    }

    [HttpGet]
    [Route("getAll")]
    public async Task<List<Role>> GetAllRoles()
    {
        return await RoleQuerier.GetAll(_dbContext);
    }

    [HttpDelete]
    [Route("delete")]
    public async Task<IActionResult> DeleteRole([FromQuery] int roleId)
    {
        var role = await RoleQuerier.GetById(_dbContext, roleId);
        if (role == null)
            return NotFound();

        _dbContext.Remove(role);
        await _dbContext.SaveChangesAsync();
        return Ok();
    }
}

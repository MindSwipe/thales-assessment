using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThalesAssessment.ApiModels.RequestModels;
using ThalesAssessment.DataAccess;
using ThalesAssessment.Entities;
using ThalesAssessment.Queries;

namespace ThalesAssessment.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class PersonController : ControllerBase
{
    private readonly AssessmentContext _dbContext;

    public PersonController(AssessmentContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    [Route("create")]
    public async Task CreatePerson([FromBody] CreatePerson personRequest)
    {
        var person = personRequest.ToPerson();
        var roles = await RoleQuerier.GetByIds(_dbContext, personRequest.RoleIds);
        person.Roles = roles;

        _dbContext.Persons.Update(person);
        await _dbContext.SaveChangesAsync();
    }

    [HttpGet]
    [Route("getAll")]
    public async Task<List<Person>> GetAllPersons()
    {
        return await PersonQuerier.GetAll(_dbContext);
    }

    [HttpGet]
    [Route("get")]
    public async Task<ActionResult<Person>> GetById([FromQuery] int id)
    {
        var person = await PersonQuerier.GetById(_dbContext, id);
        if (person == null)
            return NotFound();

        return Ok(person);
    }

    [HttpPost]
    [Route("assignRoleToPerson")]
    public async Task<IActionResult> AssignRoleToPerson([FromBody] AssignRoleToPersonModel assignRoleToPerson)
    {
        var person = await PersonQuerier.GetById(_dbContext, assignRoleToPerson.PersonId);
        if (person == null)
            return BadRequest("Person was not found");

        var role = await RoleQuerier.GetById(_dbContext, assignRoleToPerson.RoleId);
        if (role == null)
            return BadRequest("Role was not found");

        person.AssignRole(role);
        await _dbContext.SaveChangesAsync();

        return Ok(person);
    }

    [HttpDelete]
    [Route("delete")]
    public async Task<IActionResult> DeletePerson([FromQuery] int personId)
    {
        var person = await PersonQuerier.GetById(_dbContext, personId);
        if (person == null)
            return NotFound();

        _dbContext.Remove(person);
        await _dbContext.SaveChangesAsync();
        return Ok();
    }
}

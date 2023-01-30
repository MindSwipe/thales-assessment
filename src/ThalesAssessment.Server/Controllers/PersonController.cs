using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public async Task CreatePerson([FromBody] Person person)
    {
        _dbContext.Persons.Update(person);
        await _dbContext.SaveChangesAsync();
    }

    [HttpGet]
    [Route("getAll")]
    public async Task<List<Person>> GetAllPersons()
    {
        return await PersonQuerier.GetAll(_dbContext);
    }

    [HttpPost]
    [Route("assignRoleToPerson")]
    public async Task AssignRoleToPerson()
    {

    }
}

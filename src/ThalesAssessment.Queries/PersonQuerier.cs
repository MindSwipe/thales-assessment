using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThalesAssessment.DataAccess.Entities;
using ThalesAssessment.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace ThalesAssessment.Queries;

public static class PersonQuerier
{
    public static async Task<List<Person>> GetAll(AssessmentContext context)
    {
        return await context.Persons
            .Include(x => x.Roles)
            .ToListAsync();
    }
}

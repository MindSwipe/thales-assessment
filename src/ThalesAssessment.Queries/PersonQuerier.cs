using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThalesAssessment.DataAccess;
using Microsoft.EntityFrameworkCore;
using ThalesAssessment.Entities;

namespace ThalesAssessment.Queries;

public static class PersonQuerier
{
    public static async Task<List<Person>> GetAll(AssessmentContext context)
    {
        return await context.Persons
            .Include(x => x.Roles)
            .ToListAsync();
    }

    public static async Task<Person?> GetById(AssessmentContext context, int personId)
    {
        return await context.Persons
            .Where(x => x.Id == personId)
            .Include(x => x.Roles)
            .FirstOrDefaultAsync();
    }
}

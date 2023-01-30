using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThalesAssessment.DataAccess;
using ThalesAssessment.Entities;

namespace ThalesAssessment.Queries;

public static class RoleQuerier
{
    public static async Task<List<Role>> GetAll(AssessmentContext context)
    {
        return await context.Roles
            .Include(x => x.Persons)
            .ToListAsync();
    }

    public static async Task<Role?> GetById(AssessmentContext context, int id)
    {
        return await context.Roles
            .Where(x => x.Id == id)
            .Include(x => x.Persons)
            .FirstOrDefaultAsync();
    }
}

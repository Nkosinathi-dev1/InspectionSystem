using InspectionService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InspectionService.Persistence.Db
{
    public class InspectionDbContext : DbContext
    {
        public InspectionDbContext(DbContextOptions<InspectionDbContext> options)
        : base(options) { }

        public DbSet<Inspection> Inspections => Set<Inspection>();
    }
}

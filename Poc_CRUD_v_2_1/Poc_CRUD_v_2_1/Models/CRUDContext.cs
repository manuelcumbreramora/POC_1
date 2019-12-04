using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Poc_CRUD_v_2_1.Models
{
    public class CRUDContext : DbContext
    {
        public CRUDContext(DbContextOptions<CRUDContext> options)
            : base(options)
        {
        }

        public DbSet<TrazaProceso> TrazaProceso { get; set; }
    }
}

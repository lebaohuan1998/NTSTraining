using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace NTSTraining.Models.Contexts
{
    public class SQLContext : DbContext
    {
        public SQLContext(DbContextOptions<SQLContext> options)
        : base(options)
        {
        }

    }
}

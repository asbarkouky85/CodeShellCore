using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Data.EntityFramework
{
    public abstract class EFRepository<TContext> where TContext : DbContext
    {
    }
}

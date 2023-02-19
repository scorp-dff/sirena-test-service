using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    internal class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public DbSet<Route> Routes { get; protected set; }
    }
}

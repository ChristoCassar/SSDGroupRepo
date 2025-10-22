using Lab1_SecureSoftware_NicholasCassar.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Lab1_SecureSoftware_NicholasCassar.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

public DbSet<Lab1_SecureSoftware_NicholasCassar.Models.JobListing> JobListing { get; set; } = default!;

public DbSet<Lab1_SecureSoftware_NicholasCassar.Models.Tool> Tool { get; set; } = default!;

public DbSet<Lab1_SecureSoftware_NicholasCassar.Models.Employee> Employee { get; set; } = default!;
}

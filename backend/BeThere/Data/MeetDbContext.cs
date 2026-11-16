using Microsoft.EntityFrameworkCore;
using MeetM.Models;

namespace MeetDb.Data;

public class MeetDbContext : DbContext
{
    public DbSet<Meet> Meets { get; set; }

    public MeetDbContext(DbContextOptions<MeetDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Meet>()
        .Property(meet => meet.MeetCode)
        .HasMaxLength(4)
        .IsRequired();
    }
    
}
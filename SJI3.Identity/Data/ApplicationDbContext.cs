﻿using Fido2Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SJI3.Identity.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<FidoStoredCredential> FidoStoredCredential => Set<FidoStoredCredential>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<FidoStoredCredential>().HasKey(m => m.Id);

        base.OnModelCreating(builder);
    }
}

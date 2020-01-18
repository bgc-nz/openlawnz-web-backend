using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aspnetapp
{
    public class Model
    {
    }

    public class BackendContext : DbContext
    {
        public DbSet<Folder> Folders { get; set; }
        public DbSet<LegalCase> Cases { get; set; }

        public BackendContext(DbContextOptions<BackendContext> options)
    : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Folder>()
             .HasMany(c => c.Cases)
             .WithOne(e => e.Folder);
        }

    }

    public class Folder
    {
        public long Id { get; set; }

        public string FolderName { get; set; }

        public ICollection<LegalCase> Cases { get; }
    }

    public class LegalCase
    {
        public long Id { get; set; }
        public long CaseId { get; set; }
        public long FolderId { get; set; }
        public Folder Folder { get; set; }
    }
}

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVCPROJE2_BLOGPROJE.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCPROJE2_BLOGPROJE.DATAACCESS.Context
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }

        public DbSet<Uye> Uyeler { get; set; }
        public DbSet<Konu> Konular { get; set; }
        public DbSet<Makale> Makaleler { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<KonuMakale>().HasKey(x => new { x.KonuID, x.MakaleID });
            modelBuilder.Entity<UyeKonu>().HasKey(x => new { x.UyeID, x.KonuID });
            base.OnModelCreating(modelBuilder); 
        }
    }
}

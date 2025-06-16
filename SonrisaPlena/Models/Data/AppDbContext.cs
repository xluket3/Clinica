using ClinicaSonrrisaPlena.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClinicaSonrrisaPlena.Models.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Persona> Personas { get; set; }
        public DbSet<Odontologo> Odontologos { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Recepcionista> Recepcionistas { get; set; }
        public DbSet<Administrador> Administradores { get; set; }
        public DbSet<Turno> Turnos { get; set; }
        public DbSet<Tratamiento> Tratamientos { get; set; }
        public DbSet<HistorialTratamiento> Historiales { get; set; }
        public DbSet<PlanTratamiento> Planes { get; set; }
        public DbSet<PasoPlan> Pasos { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Persona>().UseTptMappingStrategy();
            modelBuilder.Entity<Persona>().HasKey(p => p.Id);

            modelBuilder.Entity<Persona>().ToTable("Personas");
            modelBuilder.Entity<Odontologo>().ToTable("Odontologos");
            modelBuilder.Entity<Paciente>().ToTable("Pacientes");
            modelBuilder.Entity<Recepcionista>().ToTable("Recepcionistas");
            modelBuilder.Entity<Administrador>().ToTable("Administradores");

            modelBuilder.Entity<PlanTratamiento>()
                .HasOne(p => p.Paciente)
                .WithMany(p => p.Planes)
                .HasForeignKey(p => p.IdPaciente)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PlanTratamiento>()
                .HasOne(p => p.Odontologo)
                .WithMany(o => o.Planes)
                .HasForeignKey(p => p.IdOdontologo)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Turno>()
                .HasOne(t => t.Paciente)
                .WithMany(p => p.Turnos)
                .HasForeignKey(t => t.IdPaciente)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Turno>()
                .HasOne(t => t.Odontologo)
                .WithMany(o => o.Turnos)
                .HasForeignKey(t => t.IdOdontologo)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<HistorialTratamiento>()
                .HasOne(h => h.Paciente)
                .WithMany(p => p.Historiales)
                .HasForeignKey(h => h.IdPaciente)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<HistorialTratamiento>()
                .HasOne(h => h.Odontologo)
                .WithMany(o => o.Historiales)
                .HasForeignKey(h => h.IdOdontologo)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<HistorialTratamiento>()
                .HasOne(h => h.Tratamiento)
                .WithMany(t => t.Historiales)
                .HasForeignKey(h => h.IdTratamiento)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PasoPlan>()
                .HasOne(p => p.Plan)
                .WithMany(p => p.Pasos)
                .HasForeignKey(p => p.IdPlan)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PasoPlan>()
                .HasOne(p => p.Tratamiento)
                .WithMany(t => t.Pasos)
                .HasForeignKey(p => p.IdTratamiento)
                .OnDelete(DeleteBehavior.Restrict);
        }




    }
}

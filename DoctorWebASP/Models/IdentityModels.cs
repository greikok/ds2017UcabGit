using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DoctorWebASP.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public virtual Persona PersonaId { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Persona> Personas { get; set; }
        public DbSet<Cita> Citas { get; set; }
        public DbSet<Calendario> Calendarios { get; set; }
        public DbSet<EspecialidadMedica> EspecialidadesMedicas { get; set; }
        public DbSet<CentroMedico> CentrosMedicos { get; set; }
        public DbSet<RecursoHospitalario> RecursosHospitalarios { get; set; }
        public DbSet<Almacen> Almacenes { get; set; }
        public DbSet<UsoRecurso> UsoRecursos { get; set; }
        public DbSet<Bitacora> Bitacoras { get; set; }


        public ApplicationDbContext() : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ApplicationUser>()
            .HasOptional(p => p.PersonaId)
            .WithRequired(d => d.ApplicationUser);
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Calendario>()
            .HasOptional(c => c.Cita)
            .WithRequired(c => c.Calendario);
    }



    public static ApplicationDbContext Create()
    {
        return new ApplicationDbContext();
    }

    public System.Data.Entity.DbSet<DoctorWebASP.Models.HistoriaMedica> HistoriaMedicas { get; set; }
}
}
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using PruebaTecnicaBitnovo.Models;

namespace PruebaTecnicaBitnovo.DAL
{
    /// <summary>
    /// Clase de acceso al contexto de BBDD
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// Constructor vacío. Se indica el nombre de la cadena de conexión del fichero Web.config
        /// </summary>
        public ApplicationDbContext() : base("ApplicationDbContext")
        {
        }

        /// <summary>
        /// Entidades Usuario
        /// </summary>
        public DbSet<Usuario> Usuarios { get; set; }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}

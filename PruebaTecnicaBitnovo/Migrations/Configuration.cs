namespace PruebaTecnicaBitnovo.Migrations
{
    using PruebaTecnicaBitnovo.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PruebaTecnicaBitnovo.DAL.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(PruebaTecnicaBitnovo.DAL.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
            var users = new List<Usuario>
            {
                new Usuario(){NombreUsuario="jaayala",Nombre="José Antonio",Apellidos="Ayala Nadales",Balance=1300, Administrador=true, Password="1234"},
                new Usuario(){NombreUsuario="iblori",Nombre="Isabel",Apellidos="Blanco Ortíz",Balance=0,Administrador=false, Password="1234"}
            };

            users.ForEach(u => context.Usuarios.AddOrUpdate(u));
            context.SaveChanges();
        }
    }
}

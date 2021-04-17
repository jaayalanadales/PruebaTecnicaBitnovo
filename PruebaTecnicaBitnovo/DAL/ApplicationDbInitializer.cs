using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using PruebaTecnicaBitnovo.Models;

namespace PruebaTecnicaBitnovo.DAL
{
    public class ApplicationDbInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {

            var users = new List<Usuario>
            {
                new Usuario(){NombreUsuario="jaayala",Nombre="José Antonio",Apellidos="Ayala Nadales",Balance=1300, Administrador=true, Password="1234"},
                new Usuario(){NombreUsuario="iblori",Nombre="Isabel",Apellidos="Blanco Ortíz",Balance=0,Administrador=false, Password="1234"}
            };

            users.ForEach(u => context.Usuarios.Add(u));
            context.SaveChanges();

        }
        
    }
}

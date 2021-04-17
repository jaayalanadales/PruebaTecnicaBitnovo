using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PruebaTecnicaBitnovo.Models
{
    /// <summary>
    /// Datos de login del Usuario
    /// </summary>
    public class UsuarioLogin
    {
        /// <summary>
        /// Nombre de usuario
        /// </summary>
        public string NombreUsuario { get; set; }
        
        /// <summary>
        /// Contraseña
        /// </summary>
        public string Password { get; set; }
    }
}
using PruebaTecnicaBitnovo.DAL;
using PruebaTecnicaBitnovo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace PruebaTecnicaBitnovo.Controllers
{
    /// <summary>
    /// Controlador para el login de usuarios
    /// </summary>
    public class LoginController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // POST: api/Login
        /// <summary>
        /// Loguea al usuario
        /// </summary>
        /// <remarks>
        /// Genera un token JWT si el usuario con el nombreUsuario y el password dados existen en BBDD
        /// </remarks>
        /// <param name="usuarioLogin">Datos de login del usuario (nombreUsuario y password)</param>
        /// <response code="401">Unauthorized. El usuario no existe o la contraseña es incorrecta.</response> 
        [HttpPost]
        [AllowAnonymous]
        public IHttpActionResult Login(UsuarioLogin usuarioLogin)
        {
            if (usuarioLogin == null)
                return BadRequest("Usuario y Contraseña requeridos.");


            var userInfo = AutenticarUsuario(usuarioLogin.NombreUsuario, usuarioLogin.Password);
            if (userInfo != null)
            {
                TokenGenerator tokenGenerator = new TokenGenerator();
                return Ok(new { token = tokenGenerator.GenerarTokenJWT(userInfo) });
            }
            else
            {
                return Unauthorized();
            }
        }

        /// <summary>
        /// Comprueba si las credenciales de usuario son correctas
        /// </summary>
        /// <param name="username">Nombre de usuario</param>
        /// <param name="password">Contraseña</param>
        private Usuario AutenticarUsuario(string username, string password)
        {
            Usuario usuario = db.Usuarios.Where(u => u.NombreUsuario == username && u.Password == password).FirstOrDefault();
            if (usuario == null) //Si el usuario no existe
            {
                return null;
            }
            else
            {
                return usuario;
            }
        }
    }
}

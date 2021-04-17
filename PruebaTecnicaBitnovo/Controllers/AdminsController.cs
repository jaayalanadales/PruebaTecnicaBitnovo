using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Newtonsoft.Json.Linq;
using PruebaTecnicaBitnovo.DAL;
using PruebaTecnicaBitnovo.Models;

namespace PruebaTecnicaBitnovo.Controllers
{
    /// <summary>
    /// Controlador con acciones asignadas sólo a los administradores
    /// </summary>
    [Authorize]
    [AuthorizeAdmin]
    public class AdminsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // 
        // GET: api/admin/users
        /// <summary>
        /// Obtiene datos de todos los usuarios del sistema.
        /// </summary>
        /// <remarks>
        /// Obtiene datos de todos los usuarios del sistema, sean administradores o usuarios.
        /// </remarks>
        /// <response code="401">Unauthorized. El usuario no es admin, el token JWT es incorrecto o no se ha indicado.</response>              
        /// <response code="200">OK. Devuelve el objeto solicitado.</response>        
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>
        [HttpGet]
        [Route("api/admin/users")]
        public IQueryable<Usuario> GetUsuarios()
        {
            return db.Usuarios;
        }

        // 
        // GET: api/admin/users/1
        /// <summary>
        /// Obtiene datos de un usuario por su id.
        /// </summary>
        /// <remarks>
        /// Obtiene datos todos los datos de un usuario a través de su id.
        /// </remarks>
        /// <param name="id">ID del usuario</param>
        /// <response code="401">Unauthorized. El usuario no es admin, el token JWT es incorrecto o no se ha indicado.</response>              
        /// <response code="200">OK. Devuelve el objeto solicitado.</response>        
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>
        [HttpGet]
        [Route("api/admin/users/{id}")]
        [ResponseType(typeof(Usuario))]
        public async Task<IHttpActionResult> GetUsuario(int id)
        {
            Usuario usuario = await db.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }

        // 
        // PUT: api/admin/users/1
        /// <summary>
        /// Modifica los datos de un usuario.
        /// </summary>
        /// <remarks>
        /// Modifica los datos de un usuario mediante su id. Los datos a modificar se proporcionan en el cuerpo de la petición, en formato JSON.
        /// El balance no puede ser modificado mediante este método.
        /// </remarks>
        /// <param name="id">ID del usuario</param>
        /// <param name="usuario">Nuevos datos del usuario</param>
        /// <response code="401">Unauthorized. El usuario no es admin, el token JWT es incorrecto o no se ha indicado.</response>              
        /// <response code="200">OK. Devuelve el objeto solicitado.</response>        
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>
        /// <response code="400">Solicitud errónea. El usuario no existe.</response>
        [HttpPut]
        [Route("api/admin/users/{id}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUsuario([FromUri] int id, Usuario usuario)
        {
            if (UsuarioExists(usuario.NombreUsuario))
                return BadRequest("Nombre de usuario en uso");

            Usuario userData = db.Usuarios.Find(id);

            if(userData != null)
            {
                userData.NombreUsuario = usuario.NombreUsuario;
                userData.Nombre = usuario.Nombre;
                userData.Apellidos = usuario.Apellidos;
                userData.Administrador = usuario.Administrador;
                userData.Password = "1234";
            }

            await db.SaveChangesAsync();

            return Ok(userData);
        }

        // 
        // PUT: api/admin/users/1/balance/add
        /// <summary>
        /// Añade dinero al balance de un usuario.
        /// </summary>
        /// <remarks>
        /// Añade una cantidad indicada al balance del usuario. En este caso, dicha cantidad no es una trasferencia por lo que no se resta del balance de otro usuario.
        /// La cantidad a añadir se proporciona en el cuerpo de la petición, en formato JSON.
        /// </remarks>
        /// <param name="id">ID del usuario</param>
        /// <param name="data">Cuerpo de la solicitud</param>
        /// <response code="401">Unauthorized. El usuario no es admin, el token JWT es incorrecto o no se ha indicado.</response>              
        /// <response code="200">OK. Devuelve el objeto solicitado.</response>        
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>
        /// <response code="400">Solicitud errónea. El usuario no existe o la cantidad es negativa.</response>
        [HttpPut]
        [Route("api/admin/users/{id}/balance/add")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Add([FromUri] int id, [FromBody] JObject data)
        {

            //Cantidad a añadir
            double cantidad = data["cantidad"].ToObject<double>();

            if (cantidad < 0)
                return BadRequest("La cantidad a añadir debe ser positiva");

            Usuario userData = db.Usuarios.Find(id);

            if (userData != null)
            {
                userData.Balance += cantidad;
            }
            else
            {
                return BadRequest("El usuario no existe");
            }

            await db.SaveChangesAsync();

            return Ok("Se han añadido " + cantidad + " euros al usuario con nombreUsuario = " + userData.NombreUsuario);
        }

        // 
        // PUT: api/admin/users/1/balance/substract
        /// <summary>
        /// Resta dinero al balance de un usuario.
        /// </summary>
        /// <remarks>
        /// Resta una cantidad indicada al balance del usuario.
        /// La cantidad a restar se proporciona en el cuerpo de la petición, en formato JSON.
        /// </remarks>
        /// <param name="id">ID del usuario</param>
        /// <param name="data">Cuerpo de la solicitud</param>
        /// <response code="401">Unauthorized. El usuario no es admin, el token JWT es incorrecto o no se ha indicado.</response>              
        /// <response code="200">OK. Devuelve el objeto solicitado.</response>        
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>
        /// <response code="400">Solicitud errónea. El usuario no existe o la cantidad es negativa.</response>
        [HttpPut]
        [Route("api/admin/users/{id}/balance/substract")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Substract([FromUri] int id, [FromBody] JObject data)
        {

            //Cantidad a restar
            double cantidad = data["cantidad"].ToObject<double>();

            if (cantidad < 0)
                return BadRequest("La cantidad a sustraer debe ser positiva");

            Usuario userData = db.Usuarios.Find(id);

            if (userData != null)
            {
                userData.Balance -= cantidad;
            }
            else
            {
                return BadRequest("El usuario no existe");
            }

            await db.SaveChangesAsync();

            return Ok("Se han sustraído " + cantidad + " euros al usuario con nombreUsuario = " + userData.NombreUsuario);
        }

        // 
        // POST: api/admin/users
        /// <summary>
        /// Crea un nuevo usuario
        /// </summary>
        /// <remarks>
        /// Crea un nuevo usuario. El nombre de usuario debe ser único.
        /// Los datos del nuevo usuario se proporcionan en el cuerpo de la petición, en formato JSON.
        /// </remarks>
        /// <param name="usuario">Información del usuario</param>
        /// <response code="401">Unauthorized. El usuario no es admin, el token JWT es incorrecto o no se ha indicado.</response>              
        /// <response code="200">OK. Devuelve el usuario añadio.</response>        
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>
        /// <response code="400">Solicitud errónea. El nombre de usuario ya está en uso o falta algún dato requerido.</response>
        [HttpPost]
        [Route("api/admin/users")]
        [ResponseType(typeof(Usuario))]
        public async Task<IHttpActionResult> PostUsuario(Usuario usuario)
        {
            if(UsuarioExists(usuario.NombreUsuario))
                return BadRequest("Nombre de usuario en uso");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Usuarios.Add(usuario);

            await db.SaveChangesAsync();


            return CreatedAtRoute("DefaultApi", new { id = usuario.ID }, usuario);
        }

        // DELETE: api/admin/users/5
        /// <summary>
        /// Elimina un usuario por su id.
        /// </summary>
        /// <remarks>
        /// Eliminar un usuario por su id.
        /// </remarks>
        /// <param name="id">ID del usuario</param>
        /// <response code="401">Unauthorized. El usuario no es admin, el token JWT es incorrecto o no se ha indicado.</response>              
        /// <response code="200">OK. Devuelve el objeto solicitado.</response>        
        /// <response code="404">NotFound. No se ha encontrado el usuario.</response>

        [HttpDelete]
        [Route("api/admin/users/{id}")]
        [ResponseType(typeof(Usuario))]
        public async Task<IHttpActionResult> DeleteUsuario(int id)
        {
            Usuario usuario = await db.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            db.Usuarios.Remove(usuario);
            await db.SaveChangesAsync();

            return Ok(usuario);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Devuelve true si el nombre de usuario ya está en uso, false en caso contrario
        /// </summary>
        private bool UsuarioExists(string nombreUsuario)
        {
            return db.Usuarios.Count(e => e.NombreUsuario == nombreUsuario) > 0;
        }
    }
}
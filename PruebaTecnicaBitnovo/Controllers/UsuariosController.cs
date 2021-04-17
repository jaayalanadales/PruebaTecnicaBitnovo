using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using Newtonsoft.Json.Linq;
using PruebaTecnicaBitnovo.DAL;
using PruebaTecnicaBitnovo.Models;

namespace PruebaTecnicaBitnovo.Controllers
{


    /// <summary>
    /// Controlador con acciones autorizadas para los usuario
    /// </summary>
    [Authorize]
    public class UsuariosController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        // 
        // GET: api/user/balance
        /// <summary>
        /// Obtener balance del usuario logueado con el bearer.
        /// </summary>
        /// <remarks>
        /// Devuelve el balance actual de la cuenta del usuario logueado. El id de este se extrae de los claims del token.
        /// </remarks>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="200">OK. Devuelve el objeto solicitado.</response>        
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>
        [HttpGet]
        [Route("api/user/balance")]
        public double GetBalanceUsuario()
        {
            //Get user claims
            ClaimsPrincipal principal = HttpContext.Current.User as ClaimsPrincipal;
            int idUsuario = Int32.Parse(principal.FindFirst("jti").Value);

            Usuario currentUser = db.Usuarios.Find(idUsuario);

            return currentUser.Balance;

        }

        // 
        // PUT: api/user/transferirBalance
        /// <summary>
        /// Transferir la cantidad indicada de euros al balance de otro usuario a partir de su nombre de usuario.
        /// </summary>
        /// <remarks>
        /// Añade la cantidad indicada al usuario objetivo de la transferencia. Si el usuario actual no posee esa cantidad de dinero, la transferencia no se realizará.
        /// </remarks>
        /// <param name="data">Cuerpo de la solicitud</param>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="200">OK. Devuelve el objeto solicitado.</response>        
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>
        /// <response code="400">Solicitud errónea. El usuario objetivo no existe o la cantidad a transferir es superior al balance del usuario emisor.</response>
        [HttpPut]
        [Route("api/user/transferirBalance")]
        public IHttpActionResult TransferirBalance([FromBody] JObject data)
        {
            //Get user claims
            ClaimsPrincipal principal = HttpContext.Current.User as ClaimsPrincipal;

            //Nombre de usuario del token
            int idUsuario = Int32.Parse(principal.FindFirst("jti").Value);

            Usuario currentUser = db.Usuarios.Find(idUsuario);

            //Nombre de usuario que recibe la transferencia (desde el body de la request)
            string usuarioTransferencia = data["usuarioTransferencia"].ToObject<string>();

            //Cantidad a transferir
            double cantidad = data["cantidad"].ToObject<double>();

            Usuario transferUser = db.Usuarios.Where(u => u.NombreUsuario == usuarioTransferencia).FirstOrDefault();

            if (transferUser == null)
                return BadRequest("El usuario no existe");

            //Si el usuario no posee esa cantidad, no se hará la transferencia
            if (currentUser.Balance >= cantidad)
            {
                transferUser.Balance += cantidad;
                currentUser.Balance -= cantidad;

                db.SaveChanges();                
            }
            else
            {
                return BadRequest("La cantidad a transferir es mayor al balance del usuario");
            }

            return Ok("Transferencia realizada con éxito");
        }

        

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
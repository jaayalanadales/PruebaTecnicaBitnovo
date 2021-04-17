using Microsoft.IdentityModel.Tokens;
using PruebaTecnicaBitnovo.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web;

namespace PruebaTecnicaBitnovo.Controllers
{
    /// <summary>
    /// Clase que permite generar un token JWT
    /// </summary>
    public class TokenGenerator
    {
        /// <summary>
        /// Generar el token JWT para un usuario
        /// </summary>
        /// <remarks>
        /// Generar el token JWT para un usuario en base a los datos del mismo y a la jwtKey del archivo Web.Config
        /// </remarks>
        /// <param name="usuarioInfo">Información del usuario</param>
        /// <response code="401">Unauthorized. El usuario no es admin, el token JWT es incorrecto o no se ha indicado.</response>              
        /// <response code="200">OK. Devuelve el objeto solicitado.</response>        
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>
        public string GenerarTokenJWT(Usuario usuarioInfo)
        {
            // RECUPERAMOS LAS VARIABLES DE CONFIGURACIÓN
            var jwtKey = ConfigurationManager.AppSettings["JwtKey"];
            var issuer = ConfigurationManager.AppSettings["Issuer"];
            var audience = ConfigurationManager.AppSettings["Audience"];
            if (!Int32.TryParse(ConfigurationManager.AppSettings["Expires"], out int expires))
                expires = 24;


            // CREAMOS EL HEADER //
            var symmetricSecurityKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtKey));
            var signingCredentials = new SigningCredentials(
                    symmetricSecurityKey, SecurityAlgorithms.HmacSha256
                );
            var header = new JwtHeader(signingCredentials);

            // CREAMOS LOS CLAIMS //
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Jti, usuarioInfo.ID.ToString()),
                new Claim("nombreUsuario", usuarioInfo.NombreUsuario.ToString()),
                new Claim("nombre", usuarioInfo.Nombre),
                new Claim("apellidos", usuarioInfo.Apellidos),
                new Claim("admin", usuarioInfo.Administrador.ToString()),
            };

            // CREAMOS EL PAYLOAD //
            var payload = new JwtPayload(
                    issuer: issuer,
                    audience: audience,
                    claims: claims,
                    notBefore: DateTime.UtcNow,
                    // Exipra a la 24 horas.
                    expires: DateTime.UtcNow.AddHours(expires)
                );

            // GENERAMOS EL TOKEN //
            var token = new JwtSecurityToken(
                    header,
                    payload
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
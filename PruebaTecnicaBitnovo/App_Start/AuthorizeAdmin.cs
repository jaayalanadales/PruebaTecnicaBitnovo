using System.Security.Claims;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace PruebaTecnicaBitnovo
{
    /// <summary>
    /// Decorador custom para autorizar sólo a los admins
    /// </summary>
    internal class AuthorizeAdmin : AuthorizeAttribute
    {
        /// <summary>
        /// Indica si el usuario está autorizado. En este caso, el usuario debe ser Administrador
        /// </summary>
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            if (!base.IsAuthorized(actionContext)) return false;

            //Get user claims
            ClaimsPrincipal principal = HttpContext.Current.User as ClaimsPrincipal;

            bool isAuthorized = bool.Parse(principal.FindFirst("admin").Value);

            return isAuthorized;
        }
    }
}
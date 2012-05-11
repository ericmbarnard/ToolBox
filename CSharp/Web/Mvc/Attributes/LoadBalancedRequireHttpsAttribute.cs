using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using RequireHttpsAttributeBase = System.Web.Mvc.RequireHttpsAttribute;

namespace Web.Mvc.Attributes
{
    /// <summary>
    /// Custom Override Attribute that handles load-balanced (cough AppHarbor) deployments
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true,
        AllowMultiple = false)]
    public class RequireHttpsAttribute : RequireHttpsAttributeBase
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            if (filterContext.HttpContext.Request.IsSecureConnection)
            {
                return;
            }

            if (string.Equals(filterContext.HttpContext.Request.Headers["X-Forwarded-Proto"],
                "https",
                StringComparison.InvariantCultureIgnoreCase))
            {
                return;
            }

            if (filterContext.HttpContext.Request.IsLocal)
            {
                return;
            }

            HandleNonHttpsRequest(filterContext);
        }
    }
}

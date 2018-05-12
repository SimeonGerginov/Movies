using System.Reflection;
using System.Web.Mvc;

namespace Movies.Infrastructure.Attributes
{
    public class AjaxOnlyAttribute : ActionMethodSelectorAttribute
    {
        public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
        {
            return controllerContext.RequestContext.HttpContext.Request.IsAjaxRequest();
        }
    }
}

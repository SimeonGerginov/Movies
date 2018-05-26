using System.Web.Mvc;
using Movies.Infrastructure.Attributes;

namespace Movies.Web.Areas.Admin.Controllers.Abstraction
{
    [AuthorizeAdmin]
    public class AdminController : Controller
    {
    }
}
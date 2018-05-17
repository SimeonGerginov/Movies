using System.Web.Mvc;
using Movies.Infrastructure.Attributes;

namespace Movies.Areas.Admin.Controllers.Abstraction
{
    [AuthorizeAdmin]
    public class AdminController : Controller
    {
    }
}
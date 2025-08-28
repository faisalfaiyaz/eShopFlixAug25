using eShopFlix.Web.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace eShopFlix.Web.Areas.User.Controllers;

[CustomAuthorization(Roles ="User")]
[Area("User")]
public class BaseController : Controller
{
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalCapstone.Controllers
{
    public class RolesController : UsersController
    {
        // GET: Roles
        public ActionResult Index()
        {

            if (User.Identity.IsAuthenticated)
            {


                if (!IsAdminUser())
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

            var Roles = db.Roles.ToList();
            return View(Roles);

        }
    }
}
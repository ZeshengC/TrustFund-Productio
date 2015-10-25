using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrustFund.Web.Core;

namespace TrustFund.Web.Controllers.MVC
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("home")]
    public class HomeController : ViewControllerBase
    {

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("my")]
        [Authorize]
        public ActionResult MyAccount()
        {
            return View();
        }
	}
}
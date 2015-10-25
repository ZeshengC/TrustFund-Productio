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
    [RoutePrefix("Customer")]
    public class CustomerController : ViewControllerBase
    {
        [HttpGet]
        [Route("uploadfile")]
        public ActionResult UploadFile()
        {
            return View();
        }
	}
}
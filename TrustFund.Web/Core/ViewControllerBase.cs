using Core.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TrustFund.Web.Core
{
    public class ViewControllerBase:Controller
    {
        List<IServiceContract> _DispasobleServices;

        protected virtual void RegisterServices(List<IServiceContract> disposableServices)
        {

        }

        List<IServiceContract> DisposableServices
        {
            get
            {
                if (_DispasobleServices == null)
                    _DispasobleServices = new List<IServiceContract>();
                return _DispasobleServices;
            }
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            RegisterServices(DisposableServices);
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            foreach(var service in DisposableServices)
            {
                if (service != null && service is IDisposable)
                    (service as IDisposable).Dispose();
            }
        }
    }
}
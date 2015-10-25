using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Common.ServiceModel
{
    public class UserClientBase<T>:ClientBase<T> where T: class
    {
        public UserClientBase()
        {
            string username = Thread.CurrentPrincipal.Identity.Name;
            Endpoint.EndpointBehaviors.Add(new CustomEndpointBehavior(username));
        }
    }
}

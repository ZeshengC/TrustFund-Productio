using Core.Common.Contracts;
using Core.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace TrustFund.Web.Core
{
    public class ApiControllerBase:ApiController,IServiceAwareController
    {
        List<IServiceContract> _DisposableServices;
        protected virtual void RegisterServices(List<IServiceContract> disposableServices)
        {

        }
     
        public void RegisterDisposableServices(List<global::Core.Common.Contracts.IServiceContract> disposableServices)
        {
            RegisterServices(disposableServices);
        }

        public List<global::Core.Common.Contracts.IServiceContract> DisposableServices
        {
            get
            {
                if (_DisposableServices == null)
                    _DisposableServices = new List<IServiceContract>();
                return _DisposableServices;
            }
        }

        protected void ValidateAuthoizedUser(string userRequested)
        {
            string userLoggedIn = User.Identity.Name;
            if(userLoggedIn != userRequested)
                throw new SecurityException("Attempting to access data for another user.");
        }

        protected HttpResponseMessage GetHttpResponse(HttpRequestMessage request, Func<HttpResponseMessage> codeToExecute)
        {
            HttpResponseMessage response = null;
            try
            {
                response = codeToExecute.Invoke();
            }
            catch(SecurityException ex)
            {
                response = request.CreateResponse(HttpStatusCode.Unauthorized, ex.Message);
            }
            catch(FaultException<AuthorizationValidationException> ex)
            {
                response = request.CreateResponse(HttpStatusCode.Unauthorized, ex.Message);
            }
            catch(FaultException<UploadedFileTypeException> ex)
            {
                response = request.CreateResponse(HttpStatusCode.UnsupportedMediaType, ex.Message);
            }
            catch(FaultException ex)
            {
                response = request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            catch(Exception ex)
            {
                response = request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

            return response;
        }

        
    }
}
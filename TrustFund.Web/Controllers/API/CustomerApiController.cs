using Core.Common.Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using TrustFund.Client.Contracts;
using TrustFund.Client.Entities;
using TrustFund.Web.Core;

namespace TrustFund.Web.Controllers.API
{
    [Export]
    [PartCreationPolicy(System.ComponentModel.Composition.CreationPolicy.NonShared)]
    [Authorize]
    [RoutePrefix("api/customer")]
    [UsesDisposableService]
    public class CustomerApiController : ApiControllerBase
    {
        private ICustomerFileService _CustomerFileService;
        [ImportingConstructor]
        public CustomerApiController(ICustomerFileService customerFileService)
        {
            _CustomerFileService = customerFileService;
        }

        protected override void RegisterServices(List<global::Core.Common.Contracts.IServiceContract> disposableServices)
        {
            disposableServices.Add(_CustomerFileService);
        }

        [HttpGet]
        [Route("uploadedFiles")]
        public HttpResponseMessage GetUploadedFiles(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
                {
                    HttpResponseMessage response = null;
                    CustomerFile[] files = _CustomerFileService.GetCustomerFiles();
                    response = request.CreateResponse<CustomerFile[]>(HttpStatusCode.OK, files);
                    return response;
                });
        }
<<<<<<< HEAD

        [HttpPost]
        [Route("upload")]
        public async Task<HttpResponseMessage> UploadFile(HttpRequestMessage request)
        {
            HttpResponseMessage response = null;

            
            if (!request.Content.IsMimeMultipartContent())
            {
                UploadedFileTypeException ex = new UploadedFileTypeException("Uploaded file type is not supported!");
                throw new FaultException<UploadedFileTypeException>(ex, ex.Message);
            }
            var uploadPath = HttpContext.Current.Server.MapPath("~/UploadedFiles");
            var multipartFormDataStreamProvider = new UploadMultipartFormProvider(uploadPath);
            await request.Content.ReadAsMultipartAsync(multipartFormDataStreamProvider);

            string[] names = multipartFormDataStreamProvider.FileData.Select(multiPartData => multiPartData.LocalFileName).ToArray();
            response = request.CreateResponse<string[]>(HttpStatusCode.OK,names);
            return response;
        }

=======
        /*
        [HttpPost]
        [Route("upload")]
        public HttpResponseMessage UploadFile(HttpRequestMessage request)
        {
            List<int> newIds = new List<int>();
            for(int i=0;i<Request.)
        }
        */
>>>>>>> origin/master


    }
}

using Core.Common.Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
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
        private IAccountService _AccountService;
        [ImportingConstructor]
        public CustomerApiController(ICustomerFileService customerFileService, IAccountService accountService)
        {
            _CustomerFileService = customerFileService;
            _AccountService = accountService;
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

        [HttpPost]
        [Route("upload")]
        public async Task<HttpResponseMessage> UploadFile(HttpRequestMessage request)
        {
            return await GetHttpResponseAsync(request, async () => 
                {
                    HttpResponseMessage responseLocal = null;
                    if (!request.Content.IsMimeMultipartContent())
                    {
                        UploadedFileTypeException ex = new UploadedFileTypeException("Uploaded file type is not supported!");
                        throw new FaultException<UploadedFileTypeException>(ex, ex.Message);
                    }

                    var uploadPath = HttpContext.Current.Server.MapPath("~/UploadedFiles/" + User.Identity.Name);
                    if(!Directory.Exists(uploadPath))
                        Directory.CreateDirectory(uploadPath);
                    var multipartFormDataStreamProvider = new UploadMultipartFormProvider(uploadPath);
                    await request.Content.ReadAsMultipartAsync(multipartFormDataStreamProvider);

                    string fullName = multipartFormDataStreamProvider.FileData.Select(multiPartData => multiPartData.LocalFileName).FirstOrDefault();
                    string name = Path.GetFileName(fullName);

                    //check if exist in the database
                    CustomerFile existFile = _CustomerFileService.GetCustomerFileByName(name);

                    CustomerFile addedFile;
                    if(existFile == null)
                    {
                        string relativePath = "/UploadedFiles/" + User.Identity.Name + "/" + name;
                        int accountId = _AccountService.GetCustomerAccountInfo(User.Identity.Name).AccountId;

                        CustomerFile file = new CustomerFile
                        {
                            FileName = name,
                            AccountId = accountId,
                            UploadDate = DateTime.Now,
                            Type = Common.FileType.LegalDoc,
                            Directory = relativePath
                        };

                        addedFile = _CustomerFileService.AddFile(file);
                    }
                    else
                    {
                        existFile.UploadDate = DateTime.Now;
                        _CustomerFileService.UpdateFile(existFile);
                        addedFile = existFile;
                    }

                    responseLocal = request.CreateResponse<CustomerFile>(HttpStatusCode.OK, addedFile);
                    return responseLocal;
                });
          }

        
            

        


    }
}

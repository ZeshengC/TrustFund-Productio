using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;
using TrustFund.Web.Core;
using TrustFund.Web.Models;

namespace TrustFund.Web.Binding.Models
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/account")]
    public class AccountApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public AccountApiController(ISecurityAdapter securityAdapter)
        {
            _SecurityAdapter = securityAdapter;
        }
        ISecurityAdapter _SecurityAdapter;

        [HttpPost]
        [Route("login")]
        public HttpResponseMessage Login(HttpRequestMessage request,[FromBody]AccountLoginModel accountModel)
        {
            return GetHttpResponse(request, () =>
                {
                    HttpResponseMessage response = null;
                    bool success = _SecurityAdapter.Login(accountModel.LoginEmail, accountModel.Password, accountModel.RememberMe);
                    if (success)
                        response = request.CreateResponse(HttpStatusCode.OK);
                    else
                        response = request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Unauthorized login.");

                    return response;
                });
        }
        [HttpPost]
        [Route("register/validate1")]
        public HttpResponseMessage ValidateRegistrationStep1(HttpRequestMessage request,[FromBody]AccountRegisterModel accountModel)
        {
            return GetHttpResponse(request, () =>
                {
                    HttpResponseMessage response = null;
                    List<string> errors = new List<string>();
                    List<State> states = UIHelper.GetStates();
                    State state = states.FirstOrDefault(item => item.Abbrev.ToUpper() == accountModel.State.ToUpper());
                    if (state == null)
                        errors.Add("Invalid state");

                    if (errors.Count == 0)
                        response = request.CreateResponse(HttpStatusCode.OK);
                    else
                        response = request.CreateResponse<string[]>(HttpStatusCode.BadRequest, errors.ToArray());
                    return response;
                });

        }
        [HttpPost]
        [Route("register/validate2")]
        public HttpResponseMessage ValidateRegistrationStep2(HttpRequestMessage request, [FromBody]AccountRegisterModel accountModel)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                //should actually validate all the fields here as well
                List<string> errors = new List<string>();
                if (!_SecurityAdapter.UserExists(accountModel.LoginEmail))
                    response = request.CreateResponse(HttpStatusCode.OK);
                else
                {
                    errors.Add("Login Email already exists.");
                    response = request.CreateResponse<string[]>(HttpStatusCode.BadRequest, errors.ToArray());
                }
                return response;
            });
        }

      

        [HttpPost]
        [Route("register")]
        public HttpResponseMessage CreateAccount(HttpRequestMessage request, [FromBody]AccountRegisterModel accountModel)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                if (ValidateRegistrationStep1(request, accountModel).IsSuccessStatusCode && ValidateRegistrationStep2(request, accountModel).IsSuccessStatusCode)
                {
                    _SecurityAdapter.Register(accountModel.LoginEmail, accountModel.Password,
                        propertyValues: new
                        {
                            FirstName = accountModel.FirstName,
                            LastName = accountModel.LastName,
                            Address = accountModel.Address,
                            City = accountModel.City,
                            State = accountModel.State,
                            ZipCode = accountModel.ZipCode
                           
                        }
                        );
                    _SecurityAdapter.Login(accountModel.LoginEmail, accountModel.Password, false);
                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                return response;
            });
        }

    }
}

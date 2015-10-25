using Core.Common.Contracts;
using Core.Common.Core;
using Core.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TrustFund.Business.Entities;
using System.ComponentModel.Composition;

namespace TrustFund.Business.Managers
{
    public class ManagerBase
    {
        public ManagerBase()
        {
            OperationContext context = OperationContext.Current;
            if(context != null)
            {
                try
                {
                    _loginName = context.IncomingMessageHeaders.GetHeader<string>("String", "System");
                    if (_loginName.IndexOf(@"\") > -1) _loginName = string.Empty;

                }
                catch
                {
                    _loginName = string.Empty;
                }
            }
            if(ObjectBase.Container != null )
            {
                ObjectBase.Container.SatisfyImportsOnce(this);
            }

            if (!string.IsNullOrWhiteSpace(_loginName))
                _AuthorizationAccount = LoadAuthorizationValidationAccount(_loginName);

            
        }

        protected string _loginName;
        protected Account _AuthorizationAccount = null;

        protected virtual Account LoadAuthorizationValidationAccount(string loginName)
        {
            return null;
        }

        protected void ValidationAuthorization(IAccountOwnedEntity entity)
        {
            if(_AuthorizationAccount != null)
            {
                if(_loginName != string.Empty && entity.OwnedAccountId != _AuthorizationAccount.AccountId)
                {
                    AuthorizationValidationException ex = new AuthorizationValidationException("Attempt to access a secure record with improper user authorization validation.");
                    throw new FaultException<AuthorizationValidationException>(ex, ex.Message);
                }
            }
        }

        protected T ExecuteFaultHandledOperation<T>(Func<T> codetoExecute)
        {
            try
            {
                return codetoExecute.Invoke();
            }
            catch(AuthorizationValidationException ex)
            {
                throw new FaultException<AuthorizationValidationException>(ex, ex.Message);
            }
            catch(FaultException ex)
            {
                throw ex;
            }
            catch(Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        protected void ExecuteFaultHandledOperation(Action codetoExecute)
        {
            try
            {
                codetoExecute.Invoke();
            }
            catch(AuthorizationValidationException ex)
            {
                throw new FaultException<AuthorizationValidationException>(ex, ex.Message);
            }
            catch(FaultException ex)
            {
                throw ex;
            }
            catch(Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
    }
}

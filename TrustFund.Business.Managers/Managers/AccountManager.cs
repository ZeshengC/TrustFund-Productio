using Core.Common.Contracts;
using Core.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using TrustFund.Business.Contracts;
using TrustFund.Business.Entities;
using TrustFund.Data.Contracts.Repository_Interface;

namespace TrustFund.Business.Managers
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
                     ConcurrencyMode = ConcurrencyMode.Multiple,
                     ReleaseServiceInstanceOnTransactionComplete = false)]
    public class AccountManager:ManagerBase,IAccountService
    {
        public AccountManager()
        {

        }

        public AccountManager(IDataRepositoryFactory dataRepositoryFactory)
        {
            _DataRepositoryFactory = dataRepositoryFactory;
        }

        [Import]
        IDataRepositoryFactory _DataRepositoryFactory;

        protected override Account LoadAuthorizationValidationAccount(string loginName)
        {
            IAccountRepository accountRepository = _DataRepositoryFactory.GetDataRepository<IAccountRepository>();
            Account authAcct = accountRepository.GetByLogin(loginName);
            if (authAcct == null)
            {
                NotFoundException ex = new NotFoundException(string.Format("Cannot find account for login name {0} to use for security trimming.", loginName));
                throw new FaultException<NotFoundException>(ex, ex.Message);
            }

            return authAcct;
        }

        public Entities.Account GetCustomerAccountInfo(string loginEmail)
        {
            return ExecuteFaultHandledOperation(() =>
                {
                    IAccountRepository accountRepository = _DataRepositoryFactory.GetDataRepository<IAccountRepository>();

                    Account accountEntity = accountRepository.GetByLogin(loginEmail);
                    if (accountEntity == null)
                    {
                        NotFoundException ex = new NotFoundException(string.Format("Account with login {0} is not in database", loginEmail));
                        throw new FaultException<NotFoundException>(ex, ex.Message);
                    }

                    ValidationAuthorization(accountEntity);
                    return accountEntity;
                });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void UpdateCustomerAccountInfo(Entities.Account account)
        {
            ExecuteFaultHandledOperation(() =>
                {
                    IAccountRepository accountRepository = _DataRepositoryFactory.GetDataRepository<IAccountRepository>();
                    ValidationAuthorization(account);
                    Account updateAccount = accountRepository.Update(account);
                });
        }
    }
}

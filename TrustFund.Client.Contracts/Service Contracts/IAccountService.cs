using Core.Common.Contracts;
using Core.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using TrustFund.Client.Entities;

namespace TrustFund.Client.Contracts
{
    [ServiceContract]
    public interface IAccountService: IServiceContract
    {
        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        [FaultContract(typeof(AuthorizationValidationException))]
        Account GetCustomerAccountInfo(string loginEmail);

        [OperationContract]
        [FaultContract(typeof(AuthorizationValidationException))]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void UpdateCustomerAccountInfo(Account account);

        #region Async operations
        Task<Account> GetCustomerAccountInfoAsync(string loginEmail);

        Task UpdateCustomerAccountInfoAsync(Account account);
        #endregion
    }
}

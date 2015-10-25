using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrustFund.Client.Contracts;
using Core.Common.ServiceModel;

namespace TrustFund.Client.Proxies
{
    public class AccountClient:UserClientBase<IAccountService>, IAccountService
    {

        public Entities.Account GetCustomerAccountInfo(string loginEmail)
        {
            return Channel.GetCustomerAccountInfo(loginEmail);
        }

        public void UpdateCustomerAccountInfo(Entities.Account account)
        {
            Channel.UpdateCustomerAccountInfo(account);
        }

        public Task<Entities.Account> GetCustomerAccountInfoAsync(string loginEmail)
        {
            return Channel.GetCustomerAccountInfoAsync(loginEmail);
        }

        public Task UpdateCustomerAccountInfoAsync(Entities.Account account)
        {
            return Channel.UpdateCustomerAccountInfoAsync(account);
        }
    }
}

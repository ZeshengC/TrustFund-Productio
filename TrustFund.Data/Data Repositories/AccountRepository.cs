using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrustFund.Business.Entities;
using TrustFund.Data.Contracts.Repository_Interface;

namespace TrustFund.Data.Data_Repositories
{
    [Export(typeof(IAccountRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class AccountRepository:DataRepositoryBase<Account>, IAccountRepository
    {
        public Account GetByLogin(string login)
        {
            using(TrustFundContext entityContext = new TrustFundContext())
            {
                return (from a in entityContext.AccountSet
                        where a.LoginEmail == login
                        select a).FirstOrDefault();
            }
        }

        protected override Account AddEntity(TrustFundContext entityContext, Account entity)
        {
            return entityContext.AccountSet.Add(entity);
        }

        protected override Account UpdateEntity(TrustFundContext entityContext, Account entity)
        {
            return (from e in entityContext.AccountSet
                    where e.AccountId == entity.AccountId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<Account> GetEntities(TrustFundContext entityContext)
        {
            return (from e in entityContext.AccountSet
                    select e);
        }

        protected override Account GetEntity(TrustFundContext entityContext, int id)
        {
            return (from e in entityContext.AccountSet
                    where e.AccountId == id
                    select e).FirstOrDefault();
        }
    }
}

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
    [Export(typeof(ICustomerFileRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CustomerFileRepository:DataRepositoryBase<CustomerFile>,ICustomerFileRepository
    {
        public IEnumerable<CustomerFile> GetCustomerFilesByAccount(Account account)
        {
            using(TrustFundContext entityContext  = new TrustFundContext())
            {
                return (from c in entityContext.CustomerFilesSet
                            where c.AccountId == account.AccountId
                            select c).ToList();
            }
        }

        public CustomerFile GetByName(string name)
        {
            using (TrustFundContext entityContext = new TrustFundContext())
            {
                return (from c in entityContext.CustomerFilesSet
                        where c.FileName == name
                        select c).FirstOrDefault();
            }
        }

        protected override CustomerFile AddEntity(TrustFundContext entityContext, CustomerFile entity)
        {
            return entityContext.CustomerFilesSet.Add(entity);
        }

        protected override CustomerFile UpdateEntity(TrustFundContext entityContext, CustomerFile entity)
        {
            return (from e in entityContext.CustomerFilesSet
                    where e.FileId == entity.FileId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<CustomerFile> GetEntities(TrustFundContext entityContext)
        {
            return (from e in entityContext.CustomerFilesSet
                    select e).ToList();
        }

        protected override CustomerFile GetEntity(TrustFundContext entityContext, int id)
        {
            return (from e in entityContext.CustomerFilesSet
                    where e.FileId == id
                    select e).FirstOrDefault();
        }



        
    }
}

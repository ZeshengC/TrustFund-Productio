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
    [Export(typeof(IFundManager))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class FundManagerRepository : DataRepositoryBase<FundManager>, IFundManager
    {

        protected override IEnumerable<FundManager> GetEntities(TrustFundContext entityContext)
        {
            return (from e in entityContext.FundManagerSet
                    select e);
        }

        protected override FundManager GetEntity(TrustFundContext entityContext, int id)
        {
            return (from e in entityContext.FundManagerSet
                    where e.ManagerId == id
                    select e).FirstOrDefault();
        }

        protected override FundManager AddEntity(TrustFundContext entityContext, FundManager entity)
        {
            return entityContext.FundManagerSet.Add(entity);
        }

        protected override FundManager UpdateEntity(TrustFundContext entityContext, FundManager entity)
        {
            return (from e in entityContext.FundManagerSet
                    where e.ManagerId == entity.ManagerId
                    select e).FirstOrDefault();
        }
    }
}

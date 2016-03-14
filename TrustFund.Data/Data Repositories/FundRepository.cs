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
    [Export(typeof(IFundRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class FundRepository:DataRepositoryBase<Fund>, IFundRepository
    {

        protected override IEnumerable<Fund> GetEntities(TrustFundContext entityContext)
        {
            return (from e in entityContext.FundSet
                    select e);
        }

        protected override Fund GetEntity(TrustFundContext entityContext, int id)
        {
            return (from e in entityContext.FundSet
                    where e.FundId == id
                    select e).FirstOrDefault();
        }

        protected override Fund AddEntity(TrustFundContext entityContext, Fund entity)
        {
            return entityContext.FundSet.Add(entity);
        }

        protected override Fund UpdateEntity(TrustFundContext entityContext, Fund entity)
        {
            return (from e in entityContext.FundSet
                    where e.FundId == entity.FundId
                    select e).FirstOrDefault();
        }
    }
}

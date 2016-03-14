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
    [Export(typeof(IFundPerformanceRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class FundPerformanceRepository : DataRepositoryBase<FundPerformance>, IFundPerformanceRepository
    {

        protected override IEnumerable<FundPerformance> GetEntities(TrustFundContext entityContext)
        {
            return (from e in entityContext.FundPerformanceSet
                    select e);
        }

        protected override FundPerformance GetEntity(TrustFundContext entityContext, int id)
        {
            return (from e in entityContext.FundPerformanceSet
                    where e.FundPerformanceId == id
                    select e).FirstOrDefault();
        }

        protected override FundPerformance AddEntity(TrustFundContext entityContext, FundPerformance entity)
        {
            return entityContext.FundPerformanceSet.Add(entity);
        }

        protected override FundPerformance UpdateEntity(TrustFundContext entityContext, FundPerformance entity)
        {
            return (from e in entityContext.FundPerformanceSet
                    where e.FundPerformanceId == entity.FundPerformanceId
                    select e).FirstOrDefault();
        }
    }
}

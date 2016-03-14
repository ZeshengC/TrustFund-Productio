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
    [Export(typeof(IPNLInvestmentNetPerformanceRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class PNLInvestmentNetPerformanceRepository: DataRepositoryBase<PNLInvestmentNetPerformance>, IPNLInvestmentNetPerformanceRepository
    {

        protected override IEnumerable<PNLInvestmentNetPerformance> GetEntities(TrustFundContext entityContext)
        {
            return (from e in entityContext.PNLInvestmentNetPerformanceSet
                    select e);
        }

        protected override PNLInvestmentNetPerformance GetEntity(TrustFundContext entityContext, int id)
        {
            return (from e in entityContext.PNLInvestmentNetPerformanceSet
                    where e.PerformanceId == id
                    select e).FirstOrDefault();
        }

        protected override PNLInvestmentNetPerformance AddEntity(TrustFundContext entityContext, PNLInvestmentNetPerformance entity)
        {
            return entityContext.PNLInvestmentNetPerformanceSet.Add(entity);
        }

        protected override PNLInvestmentNetPerformance UpdateEntity(TrustFundContext entityContext, PNLInvestmentNetPerformance entity)
        {
            return (from e in entityContext.PNLInvestmentNetPerformanceSet
                    where e.PerformanceId == entity.PerformanceId
                    select e).FirstOrDefault();
        }
    }
}

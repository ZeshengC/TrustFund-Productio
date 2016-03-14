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
    [Export(typeof(IPNLInvestmentRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class PNLInvestmentRepository:DataRepositoryBase<PNLInvestment>, IPNLInvestmentRepository
    {

        protected override IEnumerable<PNLInvestment> GetEntities(TrustFundContext entityContext)
        {
            return (from e in entityContext.PNLInvestmentSet
                    select e);
        }

        protected override PNLInvestment GetEntity(TrustFundContext entityContext, int id)
        {
            return (from e in entityContext.PNLInvestmentSet
                    where e.PNLInvestmentId == id
                    select e).FirstOrDefault();
        }

        protected override PNLInvestment AddEntity(TrustFundContext entityContext, PNLInvestment entity)
        {
            return entityContext.PNLInvestmentSet.Add(entity);
        }

        protected override PNLInvestment UpdateEntity(TrustFundContext entityContext, PNLInvestment entity)
        {
            return (from e in entityContext.PNLInvestmentSet
                    where e.PNLInvestmentId == entity.PNLInvestmentId
                    select e).FirstOrDefault();
        }
    }
}

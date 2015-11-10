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
    [Export(typeof(IPNLInvestorRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class PNLInvestorRepository : DataRepositoryBase<PNLInvestor>, IPNLInvestorRepository
    {

        protected override IEnumerable<PNLInvestor> GetEntities(TrustFundContext entityContext)
        {
            return (from e in entityContext.PNLInvestorSet
                    select e);
        }

        protected override PNLInvestor GetEntity(TrustFundContext entityContext, int id)
        {
            return (from e in entityContext.PNLInvestorSet
                    where e.PNLInvestorId == id
                    select e).FirstOrDefault();
        }

        protected override PNLInvestor AddEntity(TrustFundContext entityContext, PNLInvestor entity)
        {
            return entityContext.PNLInvestorSet.Add(entity);
        }

        protected override PNLInvestor UpdateEntity(TrustFundContext entityContext, PNLInvestor entity)
        {
            return (from e in entityContext.PNLInvestorSet
                    where e.PNLInvestorId == entity.PNLInvestorId
                    select e).FirstOrDefault();
        }
    }
}

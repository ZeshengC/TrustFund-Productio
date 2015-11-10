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
    [Export(typeof(IInvestorRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class InvestorRepository : DataRepositoryBase<Investor>, IInvestorRepository
    {

        protected override IEnumerable<Investor> GetEntities(TrustFundContext entityContext)
        {
            return (from e in entityContext.InvestorSet
                    select e);
        }

        protected override Investor GetEntity(TrustFundContext entityContext, int id)
        {
            return (from e in entityContext.InvestorSet
                    where e.InvestorId == id
                    select e).FirstOrDefault();
        }

        protected override Investor AddEntity(TrustFundContext entityContext, Investor entity)
        {
            return entityContext.InvestorSet.Add(entity);
        }

        protected override Investor UpdateEntity(TrustFundContext entityContext, Investor entity)
        {
            return (from e in entityContext.InvestorSet
                    where e.InvestorId == entity.InvestorId
                    select e).FirstOrDefault();
        }
    }
}

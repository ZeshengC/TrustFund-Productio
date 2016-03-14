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
    [Export(typeof(IInvestmentRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class InvestmentRepository:DataRepositoryBase<Investment>, IInvestmentRepository
    {

        protected override IEnumerable<Investment> GetEntities(TrustFundContext entityContext)
        {
            return (from e in entityContext.InvestmentSet
                    select e);
        }

        protected override Investment GetEntity(TrustFundContext entityContext, int id)
        {
            return (from e in entityContext.InvestmentSet
                    where e.InvestmentId == id
                    select e).FirstOrDefault();
        }

        protected override Investment AddEntity(TrustFundContext entityContext, Investment entity)
        {
            return entityContext.InvestmentSet.Add(entity);
        }

        protected override Investment UpdateEntity(TrustFundContext entityContext, Investment entity)
        {
            return (from e in entityContext.InvestmentSet
                    where e.InvestmentId == entity.InvestmentId
                    select e).FirstOrDefault();
        }
    }
}

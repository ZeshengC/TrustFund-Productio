using Core.Common.Contracts;
using Core.Common.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrustFund.Data
{
    public abstract class DataRepositoryBase<T>: DataRepositoryBase<T,TrustFundContext>
        where T: class, IIdentifiableEntity, new()
    {
    }
}

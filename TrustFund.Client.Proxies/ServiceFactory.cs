using Core.Common.Contracts;
using Core.Common.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;

namespace TrustFund.Client.Proxies
{
    [Export(typeof(IServiceContract))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ServiceFactory:IServiceFactory
    {
        public T CreateClient<T>() where T: IServiceContract
        {
            return ObjectBase.Container.GetExportedValue<T>();
        }
    }
}

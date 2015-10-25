using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrustFund.Data.Data_Repositories;

namespace TrustFund.Business.Bootstrapper
{
    public static class MEFLoader
    {
        public static CompositionContainer Init()
        {
            AggregateCatalog catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(AccountRepository).Assembly));
            CompositionContainer container = new CompositionContainer(catalog);

            return container;
        }
    }
}

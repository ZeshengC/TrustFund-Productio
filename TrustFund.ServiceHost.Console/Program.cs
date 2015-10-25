using Core.Common.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrustFund.Business.Bootstrapper;
using SM = System.ServiceModel;
using System.IO;
using TrustFund.Business.Managers;

namespace TrustFund.ServiceHost
{
    class Program
    {
        static void Main(string[] args)
        {
            ObjectBase.Container = MEFLoader.Init();
            Console.WriteLine("Starting up services...");
            Console.WriteLine("");

            SM.ServiceHost hostAccountManager = new SM.ServiceHost(typeof(AccountManager));
            SM.ServiceHost hostCustomerFileManager = new SM.ServiceHost(typeof(CustomerFileManager));

            StartService(hostAccountManager, "AccountManager");
            StartService(hostCustomerFileManager, "CustomerFileManager");

            Console.WriteLine("");
            Console.WriteLine("Press [Enter] to exit.");
            Console.ReadLine();
            Console.WriteLine("");

            StopService(hostAccountManager, "AccountManager");
         
        }

        static void StartService(SM.ServiceHost host, string serviceDescription)
        {
            host.Open();
            Console.WriteLine("Service '{0}' started", serviceDescription);

            foreach(var endpoint in host.Description.Endpoints)
            {
                Console.WriteLine(string.Format("Listening on endpoint:"));
                Console.WriteLine(string.Format("Address: {0}", endpoint.Address.Uri.ToString()));
                Console.WriteLine(string.Format("Binding: {0}", endpoint.Binding.Name));
                Console.WriteLine(string.Format("Contract: {0}", endpoint.Contract.ConfigurationName));
            }
            Console.WriteLine();
        }

        static void StopService(SM.ServiceHost host, string serviceDescription)
        {
            host.Close();
            Console.WriteLine("Service '{0}' stopped.", serviceDescription);
        }
    }
}

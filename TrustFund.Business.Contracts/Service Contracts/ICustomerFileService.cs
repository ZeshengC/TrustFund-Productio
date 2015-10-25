using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using TrustFund.Business.Entities;

namespace TrustFund.Business.Contracts
{
    [ServiceContract]
    public interface ICustomerFileService
    {
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteFile(int fileId);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        CustomerFile AddFile(CustomerFile file);

        [OperationContract]
        CustomerFile[] GetCustomerFiles();

        [OperationContract]
        CustomerFile GetCustomerFileById(int FileId);


        
        
    }
}

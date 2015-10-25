using Core.Common.Contracts;
using Core.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using TrustFund.Client.Entities;


namespace TrustFund.Client.Contracts
{
    [ServiceContract]
    public interface ICustomerFileService:IServiceContract
    {
        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        [FaultContract(typeof(AuthorizationValidationException))]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteFile(int fileId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        [FaultContract(typeof(AuthorizationValidationException))]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        CustomerFile AddFile(CustomerFile file);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        [FaultContract(typeof(AuthorizationValidationException))]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void UpdateFile(CustomerFile file);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        [FaultContract(typeof(AuthorizationValidationException))]
        CustomerFile[] GetCustomerFiles();

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        [FaultContract(typeof(AuthorizationValidationException))]
        CustomerFile GetCustomerFileById(int FileId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        [FaultContract(typeof(AuthorizationValidationException))]
        CustomerFile GetCustomerFileByName(string name);

        #region Async operations

        Task DeleteFileAsync(int fileId);

        Task<CustomerFile> AddFileAsync(CustomerFile file);

        Task UpdateFileAsync(CustomerFile file);

        Task<CustomerFile[]> GetCustomerFilesAsync();

        Task<CustomerFile> GetCustomerFileByIdAsync(int FileId);

        Task<CustomerFile> GetCustomerFileByNameAsync(string name);
        #endregion

    }
}

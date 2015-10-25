using Core.Common.Contracts;
using Core.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using TrustFund.Business.Contracts;
using TrustFund.Business.Entities;
using TrustFund.Data.Contracts.Repository_Interface;

namespace TrustFund.Business.Managers
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
                     ConcurrencyMode = ConcurrencyMode.Multiple,
                     ReleaseServiceInstanceOnTransactionComplete = false)]
    public class CustomerFileManager : ManagerBase, ICustomerFileService
    {
        public CustomerFileManager()
        {

        }

        public CustomerFileManager(IDataRepositoryFactory dataRepositoryFactory)
        {
            _DataRepositoryFactory = dataRepositoryFactory;
        }

        [Import]
        IDataRepositoryFactory _DataRepositoryFactory;

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteFile(int fileId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                ICustomerFileRepository customerFileRepository = _DataRepositoryFactory.GetDataRepository<ICustomerFileRepository>();
                customerFileRepository.Remove(fileId);
            });
           
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public CustomerFile AddFile(CustomerFile file)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                ICustomerFileRepository customerFileRepository = _DataRepositoryFactory.GetDataRepository<ICustomerFileRepository>();
                CustomerFile addedFile = customerFileRepository.add(file);
                return addedFile;
            });
        }

        public CustomerFile[] GetCustomerFiles()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                IAccountRepository accountRepository = _DataRepositoryFactory.GetDataRepository<IAccountRepository>();
                ICustomerFileRepository customerFileRepository = _DataRepositoryFactory.GetDataRepository<ICustomerFileRepository>();

                Account account = accountRepository.GetByLogin(_loginName);
                if (account == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("No account found for login {0}", _loginName));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }
                ValidationAuthorization(account);

                IEnumerable<CustomerFile> files = customerFileRepository.GetCustomerFilesByAccount(account);
                CustomerFile[] filesArray = files.ToArray<CustomerFile>();
                return filesArray;
            });
        }


        public CustomerFile GetCustomerFileById(int FileId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                ICustomerFileRepository customerFileRepository = _DataRepositoryFactory.GetDataRepository<ICustomerFileRepository>();
                CustomerFile file = customerFileRepository.Get(FileId);
                return file;
            });
        }

        public CustomerFile GetCustomerFileByName(string name)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                ICustomerFileRepository customerFileRepository = _DataRepositoryFactory.GetDataRepository<ICustomerFileRepository>();
                CustomerFile file = customerFileRepository.GetByName(name);
                return file;
            });
        }

        public void UpdateFile(CustomerFile file)
        {
            ExecuteFaultHandledOperation(() =>
            {
                ICustomerFileRepository customerFileRepository = _DataRepositoryFactory.GetDataRepository<ICustomerFileRepository>();
                CustomerFile updatedFile = customerFileRepository.Update(file);
            });
        }


        
    }
}

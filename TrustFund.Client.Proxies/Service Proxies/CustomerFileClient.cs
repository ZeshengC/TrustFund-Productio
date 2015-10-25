using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Common.ServiceModel;
using TrustFund.Client.Contracts;
using TrustFund.Client.Entities;
namespace TrustFund.Client.Proxies.Proxies
{
    [Export(typeof(ICustomerFileService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CustomerFileClient : UserClientBase<ICustomerFileService>,ICustomerFileService
    {
        public void DeleteFile(int fileId)
        {
            Channel.DeleteFile(fileId);
        }

        public CustomerFile AddFile(CustomerFile file)
        {
            return Channel.AddFile(file);
        }

        public void UpdateFile(CustomerFile file)
        {
            Channel.UpdateFile(file);
        }

        public CustomerFile[] GetCustomerFiles()
        {
            return Channel.GetCustomerFiles();
        }

        public CustomerFile GetCustomerFileById(int FileId)
        {
            return Channel.GetCustomerFileById(FileId);
        }

        public CustomerFile GetCustomerFileByName(string name)
        {
            return Channel.GetCustomerFileByName(name);
        }

        public Task DeleteFileAsync(int fileId)
        {
            return Channel.DeleteFileAsync(fileId);
        }

        public Task<CustomerFile> AddFileAsync(CustomerFile file)
        {
            return Channel.AddFileAsync(file);
        }

        public Task<CustomerFile[]> GetCustomerFilesAsync()
        {
            return Channel.GetCustomerFilesAsync();
        }

        public Task<CustomerFile> GetCustomerFileByIdAsync(int FileId)
        {
            return Channel.GetCustomerFileByIdAsync(FileId);
        }


        public Task UpdateFileAsync(CustomerFile file)
        {
            return Channel.UpdateFileAsync(file); 
        }

        public Task<CustomerFile> GetCustomerFileByNameAsync(string name)
        {
            return Channel.GetCustomerFileByNameAsync(name);
        }
    }
}

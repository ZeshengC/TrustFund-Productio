using Core.Common.Contracts;
using Core.Common.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrustFund.Business.Bootstrapper;
using TrustFund.Business.Entities;
using TrustFund.Business.Managers;
using TrustFund.Common;
using TrustFund.Data.Contracts.Repository_Interface;

namespace TrustFund.Business.Managers.Tests
{
    [TestClass]
    public class CustomerFileManagerTest
    {
        [TestInitialize]
        public void Initialize()
        {
            ObjectBase.Container = MEFLoader.Init();
        }
        [TestMethod]
        public void AddFile()
        {
            CustomerFile file = new CustomerFile() { AccountId = 1, FileName = "Test.txt",Type = FileType.LegalDoc,UploadDate = new DateTime(2015,10,18)};
            CustomerFile addedFile = new CustomerFile() { FileId = 1, AccountId = 1, FileName = "Test.txt", Type = FileType.LegalDoc, UploadDate = new DateTime(2015, 10, 18) };
            Mock<IDataRepositoryFactory> mockDataRepositoryFactory = new Mock<IDataRepositoryFactory>();
            mockDataRepositoryFactory.Setup(mock => mock.GetDataRepository<ICustomerFileRepository>().add(file)).Returns(addedFile);

            CustomerFileManager manager = new CustomerFileManager(mockDataRepositoryFactory.Object);
            CustomerFile addFileResults =  manager.AddFile(file);

            Assert.IsTrue(addedFile == addFileResults);
        }
    }
}

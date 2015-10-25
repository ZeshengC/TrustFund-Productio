using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Core.Common.Core;
using TrustFund.Business.Bootstrapper;
using System.ComponentModel.Composition;
using TrustFund.Data.Contracts.Repository_Interface;
using System.Collections;
using TrustFund.Business.Entities;
using System.Collections.Generic;
using Core.Common.Contracts;

namespace TrustFund.Data.Tests
{

    [TestClass]
    public class DataLayerTests
    {
        [TestInitialize]
        public void Initialize()
        {
            ObjectBase.Container = MEFLoader.Init();
        }
        [TestMethod]
        public void test_repository_usage()
        {
            RepositoryTestClass repositoryTest = new RepositoryTestClass();
            IEnumerable<CustomerFile> files = repositoryTest.getFiles();
            Assert.IsTrue(files != null);
        }
        [TestMethod]
        public void test_repository_factory_usage()
        {
            RepositoryFactoryTestClass repositoryTest = new RepositoryFactoryTestClass();
            IEnumerable<CustomerFile> files = repositoryTest.getFiles();
            Assert.IsTrue(files != null);
        }

        [TestMethod]
        public void test_string_extension()
        {
            string s = "count";
            int count = s.count(true);
            Assert.IsTrue(count == 5);
        }
    }

    public class RepositoryTestClass
    {
        public RepositoryTestClass()
        {
            ObjectBase.Container.SatisfyImportsOnce(this);
        }

        [Import]
        ICustomerFileRepository _customerFileRepository;

        public IEnumerable<CustomerFile> getFiles()
        {
            IEnumerable<CustomerFile> files = _customerFileRepository.Get();
            return files;
        }
    }

    public class RepositoryFactoryTestClass
    {
        public RepositoryFactoryTestClass()
        {
            ObjectBase.Container.SatisfyImportsOnce(this);
        }

        [Import]
        IDataRepositoryFactory _DataRepositoryFactory;

        public IEnumerable<CustomerFile> getFiles()
        {
            ICustomerFileRepository _repository = _DataRepositoryFactory.GetDataRepository<ICustomerFileRepository>();
            IEnumerable<CustomerFile> files = _repository.Get();
            return files;
        }
    }

    public static class stringextension
    {
        public static int count(this string s,bool isTrue)
        {
            return s.Length;
        }
    }

}

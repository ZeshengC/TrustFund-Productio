using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Core.Common.Core;

namespace Core.Common.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            IndexerClass tc = new IndexerClass();
            int error = tc[3];
        }
    }

    class IndexerClass : ISomeInterface
    {
        private int[] arr = new int[100];


        public int this[int index]
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
    public interface ISomeInterface
    {
        // Indexer declaration: 
        int this[int index]
        {
            get;
            set;
        }
    }

   
}

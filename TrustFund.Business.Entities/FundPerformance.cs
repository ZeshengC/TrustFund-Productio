using Core.Common.Contracts;
using Core.Common.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TrustFund.Business.Entities
{
    [DataContract]
    public class FundPerformance:EntityBase,IIdentifiableEntity
    {
        [DataMember]
        public int FundPerformanceId { get; set; }
        [DataMember]
        public int FundId { get; set; }
        [DataMember]
        public DateTime Date { get; set; }
        [DataMember]
        public double BeginingBalance { get; set; }
        [DataMember]
        public double CapitalActivity { get; set; }
        [DataMember]
        public double NetPerformance { get; set; }
        [DataMember]
        public double EndingBalance { get; set; }

        public int EntityId
        {
            get
            {
                return FundPerformanceId;
            }
            set
            {
                FundPerformanceId = value;
            }
        }
    }
}

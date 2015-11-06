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
    public class PNLInvestmentNetPerformance:EntityBase,IIdentifiableEntity
    {
        [DataMember]
        public int PerformanceId { get; set; }
        [DataMember]
        public string PerformanceName { get; set; }
        [DataMember]
        public double PerformanceValue { get; set; }
        [DataMember]
        public DateTime BeginDate { get; set; }
        [DataMember]
        public DateTime EndDate { get; set; }
        [DataMember]
        public double DailyValue { get; set; }
        public int EntityId
        {
            get
            {
                return PerformanceId;
            }
            set
            {
                PerformanceId = value;
            }
        }
    }
}


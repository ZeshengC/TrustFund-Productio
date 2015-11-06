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
    public class PNLInvestment:EntityBase,IIdentifiableEntity
    {
        [DataMember]
        public int PNLInvestmentId { get; set; }
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
        [DataMember]
        public int FundId { get; set; }
        [DataMember]
        public int InvestmentId { get; set; }

        public int EntityId
        {
            get
            {
                return PNLInvestmentId;
            }
            set
            {
                PNLInvestmentId = value;
            }
        }
    }
}

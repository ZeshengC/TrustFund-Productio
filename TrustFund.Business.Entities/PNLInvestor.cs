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
    public class PNLInvestor:EntityBase,IIdentifiableEntity
    {
        public int PNLInvestorId { get; set; }
        public int InvestorId { get; set; }
        public int InvestmentId { get; set; }
        public DateTime Date { get; set; }
        public double BeginningBalance { get; set; }
        public double CapitalActivity { get; set; }
        public double NetPerformences { get; set; }
        public double EndingBalance { get; set; }

        public int EntityId
        {
            get
            {
                return PNLInvestorId;
            }
            set
            {
                PNLInvestorId = value;
            }
        }
    }
}

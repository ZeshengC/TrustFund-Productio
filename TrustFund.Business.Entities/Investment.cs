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
    public class Investment:EntityBase,IIdentifiableEntity
    {
        [DataMember]
        public int InvestmentId { get; set; }
        [DataMember]
        public string InvestmentName { get; set; }
        [DataMember]
        public int FundId { get; set; }

        public int EntityId
        {
            get
            {
                return InvestmentId;
            }
            set
            {
                InvestmentId = value;
            }
        }
    }
}

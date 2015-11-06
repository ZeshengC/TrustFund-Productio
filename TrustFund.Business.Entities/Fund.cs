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
    public class Fund:EntityBase,IIdentifiableEntity
    {
        [DataMember]
        public int FundId { get; set; }
        [DataMember]
        public string FundName { get; set; }

        public int EntityId
        {
            get
            {
                return FundId;
            }
            set
            {
                FundId = value;
            }
        }
    }
}

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
    public class Investor:EntityBase,IIdentifiableEntity,IAccountOwnedEntity
    {
        [DataMember]
        public int InvestorId { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int AccountID { get; set; }

        public int EntityId
        {
            get
            {
                return InvestorId;
            }
            set
            {
                InvestorId = value;
            }
        }

        public int OwnedAccountId
        {
            get 
            {
                return AccountID;
            }
        }
    }
}

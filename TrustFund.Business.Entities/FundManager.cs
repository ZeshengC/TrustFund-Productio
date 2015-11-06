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
    public class FundManager:EntityBase,IIdentifiableEntity,IAccountOwnedEntity
    {
        [DataMember]
        public int ManagerId { get; set; }
        [DataMember]
        public int AccountId { get; set; }
        [DataMember]        
        public string ManagerName { get; set; }
        [DataMember]        
        public int FundId { get; set; }
        public int EntityId
        {
            get
            {
                return ManagerId;
            }
            set
            {
                ManagerId = value;
            }
        }

        public int OwnedAccountId
        {
            get { return AccountId; }
        }
    }
}

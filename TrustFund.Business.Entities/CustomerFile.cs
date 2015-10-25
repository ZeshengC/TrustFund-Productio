using Core.Common.Contracts;
using Core.Common.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using TrustFund.Common;

namespace TrustFund.Business.Entities
{
    [DataContract]
    public class CustomerFile:EntityBase,IIdentifiableEntity,IAccountOwnedEntity
    {
        [DataMember]
        public int FileId { get; set; }
        [DataMember]
        public string FileName { get; set; }
        [DataMember]
        public FileType Type { get; set; }
        [DataMember]
        public DateTime UploadDate { get; set; }
        [DataMember]
        public int AccountId { get; set; }
        [DataMember]
        public string Directory { get; set; }
        public int EntityId
        {
            get
            {
                return FileId;
            }
            set
            {
                FileId = value;
            }
        }

        public int OwnedAccountId
        {
            get { return AccountId; }
        }
    }
}

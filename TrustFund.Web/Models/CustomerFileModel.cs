using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrustFund.Common;

namespace TrustFund.Web.Models
{
    public class CustomerFileModel
    {
        public int FileId { get; set; }
        public string FileName { get; set; }
        public FileType Type { get; set; }
        public DateTime UploadDate { get; set; }
        public int AccountId { get; set; }
        public string Directory { get; set; }
    }
}
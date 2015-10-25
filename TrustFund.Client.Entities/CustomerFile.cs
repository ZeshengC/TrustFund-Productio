using Core.Common.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrustFund.Common;

namespace TrustFund.Client.Entities
{
    public class CustomerFile:ObjectBase
    {
        int _FileId;
        string _FileName;
        FileType _Type;
        DateTime _UploadDate;
        int _AccountId;
        string _Directory;

        public int FileId
        {
            get { return _FileId; }
            set 
            {
                if(_FileId != value)
                {
                    _FileId = value;
                    OnPropertyChanged(() => _FileId);
                }
            }
        }
        public string FileName 
        {
            get { return _FileName; }
            set 
            {
                if(_FileName != value)
                {
                    _FileName = value;
                    OnPropertyChanged(() => _FileName);
                }
            }
        }
        public FileType Type 
        {
            get { return _Type; }
            set 
            {
                if (_Type != value)
                {
                    _Type = value;
                    OnPropertyChanged(() => _Type);
                }
            }
        }
        public DateTime UploadDate
        {
            get { return _UploadDate; }
            set 
            {
                if (_UploadDate != value)
                {
                    _UploadDate = value;
                    OnPropertyChanged(() => _UploadDate);
                }
            } 
        }
        public int AccountId
        {
            get { return _AccountId; }
            set 
            {
                if (_AccountId != value)
                {
                    _AccountId = value;
                    OnPropertyChanged(() => _AccountId);
                }
            }
        }
        public string Directory 
        {
            get { return _Directory; }
            set 
            {
                if (_Directory != value)
                {
                    _Directory = value;
                    OnPropertyChanged(() => _Directory);
                }
            }
        }
    }
}

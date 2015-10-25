using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Common.Exceptions
{
    public class UploadedFileTypeException:ApplicationException
    {
        public UploadedFileTypeException(string message):base(message)
        {

        }

        public UploadedFileTypeException(string message, Exception exception)
            : base(message, exception)
        {

        }
    }
}
